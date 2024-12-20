using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Логика взаимодействия для UserAccountPage.xaml
    /// </summary>
    public partial class UserAccountPage : Window
    {
        private string phoneNumber;
        private List<UIElement> canvasElements = new List<UIElement>();
        OrderPage order = new OrderPage();
        DataTable table = new DataTable();

        public UserAccountPage()
        {
            InitializeComponent();

        }
        public UserAccountPage(string phoneNumber)
        {
            InitializeComponent();
            this.phoneNumber = phoneNumber;
            userPhoneNumber.Text = "+" + phoneNumber;
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

        private void Create_Message_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Block_Account_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены?", "Блокировака юзера", MessageBoxButton.YesNo);
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
                var user = db.Users.FirstOrDefault(u => u.Phone_number == phoneNumber);

                if (user != null)
                {
                    var userOrders = db.Orders
                        .Where(o => o.User_id == user.id)
                        .ToList();

                    table.Columns.Add("ID заказа", typeof(int));
                    table.Columns.Add("ID юзера", typeof(int));
                    table.Columns.Add("Дата начала", typeof(string));
                    table.Columns.Add("Время начала", typeof(string));
                    table.Columns.Add("Заказ", typeof(string));
                    table.Columns.Add("Часы", typeof(int));
                    table.Columns.Add("Цена", typeof(int));

                    foreach (var order in userOrders)
                    {
                        table.Rows.Add(order.order_id, order.user_id, order.date_start, order.time_start, order.order_name, order.hour_quantity, order.total_price);
                    }

                    dataGridView.ItemsSource = table.DefaultView;

                }

            }
        }
    }
}
