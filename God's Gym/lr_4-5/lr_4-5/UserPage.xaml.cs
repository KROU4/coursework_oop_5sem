using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Window
    {
        public UserPage()
        {
            InitializeComponent();

            ApplicationContext db = new ApplicationContext();
            List<User> users = db.Users.ToList();

            usersList.ItemsSource = users;

        }

        private void UserListSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            var selectionItem = (sender as ListView).SelectedItem as User;
            
            if (selectionItem != null)
            {
                UserAccountPage accountPage = new UserAccountPage(selectionItem.Phone_number);
                AdminAccount admin = new AdminAccount(selectionItem.Phone_number);
                if (selectionItem.Phone_number == "375444444444")
                {
                    admin.Show();
                }
                else
                {
                    accountPage.Show();
                }
                Hide();
            }

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }

    }
}
