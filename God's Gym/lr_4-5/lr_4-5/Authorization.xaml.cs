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
using System.Text.RegularExpressions;

namespace lr_4_5
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            string phone_number = loginTextBox.Text.Trim();
            string pass = passwordBox.Password.Trim();
            string pattern = @"^375(25|29|33|44)\d{7}$";

            if (!Regex.IsMatch(phone_number, pattern))
            {
                loginTextBox.ToolTip = "Введите номер телефона в формате '375YYXXXXXXX'";
                loginTextBox.BorderBrush = Brushes.Red;
            }

            else if (pass.Length < 6)
            {
                passwordBox.ToolTip = "Пароль не должен быть короче 6 символов";
                passwordBox.BorderBrush = Brushes.Red;
            }
            else
            {
                loginTextBox.ToolTip = "";
                loginTextBox.BorderBrush = Brushes.White;
                passwordBox.ToolTip = "";
                passwordBox.BorderBrush = Brushes.White;

                User authUser = null;
                using (ApplicationContext db = new ApplicationContext())
                {
                    authUser = db.Users.Where(u => u.Phone_number == phone_number && u.Password == pass).FirstOrDefault();
                }

                if (authUser != null)
                {
                    loginTextBox.ToolTip = "";
                    loginTextBox.BorderBrush = Brushes.White;
                    passwordBox.ToolTip = "";
                    passwordBox.BorderBrush = Brushes.White;

                    App.Phone_Number = phone_number;

                    UserPage user = new UserPage();
                    UserAccountClient client = new UserAccountClient(phone_number);
                    if (authUser.Phone_number == "375441111111" && authUser.Password == "1111111")
                    {
                        user.Show();
                        this.Hide();
                    }
                    else
                    {

                        client.Show();
                        this.Hide();
                    }
                }
                else
                {
                    loginTextBox.ToolTip = "Нет такого пользователя. Проверьте введённые данные.";
                    loginTextBox.BorderBrush = Brushes.Red;
                    passwordBox.ToolTip = "Нет такого пользователя. Проверьте введённые данные.";
                    passwordBox.BorderBrush = Brushes.Red;
                }
            }
        }

        private void Button_Click_Sign_in(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Hide();
        }
    }
}
