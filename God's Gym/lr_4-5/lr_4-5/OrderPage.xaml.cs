using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Net;
using System.Net.Mail;
using System;
using System.Linq;

namespace lr_4_5
{
    public partial class OrderPage : Window, INotifyPropertyChanged
    {
        ApplicationContext db = new ApplicationContext();
        private ObservableCollection<string> notify;
        private ObservableCollection<string> payment;
        private string email;
        private string orderPhoneNumber;
        private string selectedNotification;
        private string offerName;
        private float offerPrice;
        private string phoneNumber;


        public const string adminEmail = "**************";
        public const String clientEmail = "**************";
        public const String adminPassword = "**************";
        public OrderPage()
        {
            InitializeComponent();

            notify = new ObservableCollection<string>()
            {
                "SMS",
                "Mail"
            };

            payment = new ObservableCollection<string>()
            {
                "Наличка",
                "Карта"
            };

            DataContext = this;

            Notify = notify;
            Payment = payment;

            phone_Number.Text = App.Phone_Number;

        }

        public ObservableCollection<string> Notify
        {
            get { return notify; }
            set
            {
                notify = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Payment
        {
            get { return payment; }
            set
            {
                payment = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public string OrderPhoneNumber
        {
            get { return orderPhoneNumber; }
            set
            {
                orderPhoneNumber = value;
                OnPropertyChanged();
            }
        }

        public string SelectedNotification
        {
            get { return selectedNotification; }
            set
            {
                selectedNotification = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsEmailEnabled));
                OnPropertyChanged(nameof(IsPhoneNumberEnabled));
            }
        }

        public string OfferName
        {
            get { return offerName; }
            set
            {
                offerName = value;
                OnPropertyChanged();
            }
        }

        public float OfferPrice
        {
            get
            {
                return offerPrice;
            }
            set
            {
                offerPrice = value;
                OnPropertyChanged();
            }
        }

        private int myNumber;

        public int MyNumber
        {
            get { return myNumber; }
            set
            {
                if (myNumber != value)
                {
                    myNumber = value;
                    OnPropertyChanged();
                }
            }
        }


        public bool IsEmailEnabled => SelectedNotification == "Mail";
        public bool IsPhoneNumberEnabled => SelectedNotification == "SMS";

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static float FinalCost(int hourQuantity, float serviceCost)
        {
            return hourQuantity * serviceCost;
        }

        private bool IsValidDateTime(string date, string time)
        {
            if (string.IsNullOrWhiteSpace(date) || string.IsNullOrWhiteSpace(time))
            {
                MessageBox.Show("Дата и время обязательны к заполнению.");
                return false;
            }

            if (!DateTime.TryParseExact(date, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out _))
            {
                MessageBox.Show("Неверный формат даты. Используйте формат: dd.MM.yyyy.");
                return false;
            }

            if (!TimeSpan.TryParse(time, out _))
            {
                MessageBox.Show("Неверный формат времени. Используйте формат: HH:mm.");
                return false;
            }

            return true;
        }

        private void SetCurrentDateTime(object sender, RoutedEventArgs e)
        {
            dateStart.Text = DateTime.Now.ToString("dd.MM.yyyy");
            timeStart.Text = DateTime.Now.ToString("HH:mm");
        }

        private void SendMail(object sender, RoutedEventArgs e)
        {
            string desiredDate = dateStart.Text.Trim();
            string desiredTime = timeStart.Text.Trim();

            if (!IsValidDateTime(desiredDate, desiredTime))
            {
                return;
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.FirstOrDefault(i => i.Phone_number == App.Phone_Number);

                if (user != null)
                {
                    int user_id = user.id;
                    int hour_quantity = int.Parse(hour.Text.Trim());
                    string order_name = serviceName.Text.Trim();
                    int total_price = (int)FinalCost(MyNumber, OfferPrice);
                    
                    
                    if (user != null)
                    {
                        bool hasExistingOrder = db.Orders.Any(o =>
                            o.Date_start == desiredDate &&
                            o.Time_start == desiredTime);                        

                        if (hasExistingOrder  )
                        {
                            MessageBox.Show("Заказ на указанную дату, время и компьютер уже существует.");
                        }

                        else
                        {                            
                            // Mail
                            float finalCost;
                            if (OfferName.Contains("абонемент"))
                            {
                                finalCost = OfferPrice;
                                total_price = (int)FinalCost(1, OfferPrice);
                                hour_quantity = 8;
                                Order order = new Order(user_id, desiredDate, desiredTime, hour_quantity, order_name, total_price);
                                db.Orders.Add(order);
                                db.SaveChanges();
                            }
                            else
                            {
                                finalCost = FinalCost(MyNumber, OfferPrice);
                                Order order = new Order(user_id, desiredDate, desiredTime, hour_quantity, order_name, total_price);
                                db.Orders.Add(order);
                                db.SaveChanges();
                            }
                            string subjectMail = "Уведомление о заказе";
                            string bodyMail = $"Ваш заказ {serviceName.Text} был успешно оформлен.\n Мы очень рады, что вы решили воспользоваться услугами нашего клуба.\n" +
                                $"Общая стоимость вашей услуги: {finalCost} BYN.\n Дата: {desiredDate}, {desiredTime}.\n Количество часов: {hour_quantity}";
                            int hourQuantity = MyNumber;
                            try
                            {
                                if (emailTextBox.Text.Length > 0 && notifyWay.Text == "Mail")
                                {
                                    var mail = Mail.CreateMail("GYM", "**************", emailTextBox.Text, subjectMail, bodyMail);
                                    Mail.SendMail("smtp.gmail.com", 587, "**************", "**************", mail);

                                    MessageBox.Show("Сообщение было отправлено вам на электронную почту.");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Не выполнено!\n" + ex.Message);
                            }
                            // Уведомление об успешном заказе
                            MessageBox.Show("Ваш заказ был успешно оформлен!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не найден.");
                    }
                   
                }

                else
                {

                }
            }
        }
    }
}
