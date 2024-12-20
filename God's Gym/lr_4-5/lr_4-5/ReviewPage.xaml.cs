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
    /// Логика взаимодействия для ReviewPage.xaml
    /// </summary>
    public partial class ReviewPage : Window
    {
        public ReviewPage()
        {
            InitializeComponent();
            ApplicationContext db = new ApplicationContext();
            List<Review> reviews = db.Reviews.ToList();
            reviewsList.ItemsSource = reviews;
        }

        private void reviewsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

    }
}
