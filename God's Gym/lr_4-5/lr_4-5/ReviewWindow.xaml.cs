using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для ReviewWindow.xaml
    /// </summary>
    public partial class ReviewWindow : Window, INotifyPropertyChanged
    {     
        public ReviewWindow()
        {
            InitializeComponent();

            DataContext = this;
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
                    OnPropertyChanged(nameof(MyNumber));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Review_Click(object sender, RoutedEventArgs e)
        {
            
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.FirstOrDefault(i => i.Phone_number == App.Phone_Number);
                
                if (user != null)
                {
                    int user_id = user.id;
                    string UserName = userName.Text.Trim();
                    int Rating = int.Parse(rate.Text.Trim());
                    string Comment = comment.Text.Trim();
                    string date = DateTime.Now.ToString();

                    Review review = new Review(user_id, UserName, Rating, Comment, date);
                    db.Reviews.Add(review);
                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show("Отправлено");
                    }
                    catch (DbUpdateException ex)
                    {
                        var innerException = ex.InnerException;
                        while (innerException != null)
                        {
                            MessageBox.Show(innerException.Message);
                            innerException = innerException.InnerException;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
