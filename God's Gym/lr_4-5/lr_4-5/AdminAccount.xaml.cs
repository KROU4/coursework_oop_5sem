using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lr_4_5
{
    /// <summary>
    /// Логика взаимодействия для AdminAccount.xaml
    /// </summary>
    public partial class AdminAccount : Window
    {
        private string phoneNumber;
        private List<UIElement> canvasElements = new List<UIElement>();
        OrderPage order = new OrderPage();
        DataTable table = new DataTable();
        public AdminAccount()
        {
            InitializeComponent();
        }

        public AdminAccount(string phoneNumber)
        {
            InitializeComponent();
            this.phoneNumber = phoneNumber;
            userPhoneNumber.Text = "+" + phoneNumber;
            App.Phone_Number = phoneNumber;
            order = new OrderPage();
            order.OrderPhoneNumber = App.Phone_Number;          
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 120)
            {
                foreach (UIElement child in Canvas1.Children)
                {
                    child.Visibility = Visibility.Collapsed;
                }
                //home.Visibility = Visibility.Visible;
            }
            else
            {
                foreach (UIElement child in Canvas1.Children)
                {
                    child.Visibility = Visibility.Visible;
                }
                //home.Visibility = Visibility.Collapsed;
            }
        }

        private void Delete_Account_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены?", "Удалить аккаунт юзера", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var account = db.Users.FirstOrDefault(u => u.Phone_number == phoneNumber);

                    if (account != null)
                    {
                        int userID = account.id;
                        db.Users.Remove(account);
                        db.SaveChanges();
                    }

                }
                UserPage page = new UserPage();
                page.Show();
                Hide();
            }
            else
            {

            }
        }

        private void Log_out_Click(object sender, RoutedEventArgs e)
        {
            UserPage page = new UserPage();
            page.Show();
            Hide();
        }

        private void Offer_Click(object sender, RoutedEventArgs e)
        {
            OffersAndPromotions offers = new OffersAndPromotions();
            offers.Show();
            Hide();
        }

        private void DataGridView_Loaded(object sender, RoutedEventArgs e)
        {
            
            using (ApplicationContext db = new ApplicationContext())
            {
                var orders = db.Orders.ToList();

                table.Columns.Add("Order ID", typeof(int));
                table.Columns.Add("User ID", typeof(int));
                table.Columns.Add("Date Start", typeof(string));
                table.Columns.Add("Time Start", typeof(string));
                table.Columns.Add("Order Name", typeof(string));
                table.Columns.Add("Hours", typeof(int));

                foreach (var order in orders)
                {
                    table.Rows.Add(order.order_id, order.user_id, order.date_start, order.time_start, order.order_name, order.hour_quantity);
                }
                dataGridView.ItemsSource = table.DefaultView;
            }
        }
    }
}