using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lr_4_5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db;
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing; // Обработчик завершения окна
            db = new ApplicationContext();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0); // 0 indicates a normal exit
        }

        private void Button_Click_Log_in(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            string phone_number = loginTextBox.Text.Trim();
            string pass = passwordBox.Password.Trim();
            string repeatPass = repeatPasswordBox.Password.Trim();

            string pattern = @"^375(25|29|33|44)\d{7}$";

            if (!Regex.IsMatch(phone_number, pattern))
            {
                loginTextBox.ToolTip = "Введите номер телефона в формате '375YYXXXXXXX'";
                loginTextBox.BorderBrush = Brushes.Red;
            }

            else if(pass.Length < 6)
            {
                passwordBox.ToolTip = "Пароль не должен быть короче 6 символов";
                passwordBox.BorderBrush = Brushes.Red;
            }

            else if(repeatPass != pass)
            {
                repeatPasswordBox.ToolTip = "Пароли должны совпадать";
                repeatPasswordBox.BorderBrush = Brushes.Red;
            } 

            else
            {
                loginTextBox.ToolTip = "";
                loginTextBox.BorderBrush = Brushes.White;
                passwordBox.ToolTip = "";
                passwordBox.BorderBrush = Brushes.White;
                repeatPasswordBox.ToolTip = "";
                repeatPasswordBox.BorderBrush = Brushes.White;

                User user = new User(phone_number, pass);

                User authUser = null;
                using (ApplicationContext db = new ApplicationContext())
                {
                    authUser = db.Users.Where(us => us.Phone_number == phone_number).FirstOrDefault();
                }

                if (authUser != null)
                {
                    loginTextBox.ToolTip = "Номер уже сущестует. Войдите или ведите другой номер телефона.";
                    loginTextBox.BorderBrush = Brushes.Red;
                }
                else
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }

            }

        }
    }
}
