using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для OffersAndPromotions.xaml
    /// </summary>
    public partial class OffersAndPromotions : Window
    {
        List<Offer> offers = new List<Offer>();
        public OffersAndPromotions()
        {
            InitializeComponent();
            offers.Add(new Offer("Утренний абонемент", "Идеальный выбор для тех, кто хочет начать свой день с тренировки. Утренний абонемент предоставляет доступ к залу с 6:00 до 12:00, включая использование всех " +
                "тренажеров, зон для растяжки и групповых тренировок. Также вы получите бесплатные напитки в фитнес-баре. Присоединяйтесь к утренним тренировкам и начните свой день активно!", 2, "06:00-12:00", Range.General));

            offers.Add(new Offer("Вечерний абонемент", "Для тех, кто предпочитает заниматься спортом вечером, мы предлагаем вечерний абонемент. Он предоставляет доступ к залу с 17:00 до 23:00. В стоимость входит " +
                "пользование всеми зонами зала, участие в групповых тренировках, а также бонус — скидка 10% на услуги сауны. Заряжайтесь энергией после рабочего дня!", 3, "17:00-23:00", Range.General));

            offers.Add(new Offer("VIP Утренний доступ", "VIP-утренний доступ создан для тех, кто ценит комфорт и эксклюзивность. Вы получите доступ в приватную зону зала с улучшенными условиями, " +
                "персональным тренером, а также бесплатные протеиновые коктейли. Этот абонемент доступен с 6:00 до 12:00 и обеспечит вам максимально приятное начало дня.", 4, "06:00-12:00", Range.VIP));

            offers.Add(new Offer("VIP Вечерний доступ", "Окунитесь в атмосферу премиального фитнеса с нашим VIP вечерним доступом! Включает индивидуальные тренировки, использование частной зоны зала и " +
                "дополнительные услуги, такие как массаж после тренировки. Абонемент действует с 17:00 до 23:00 и идеально подходит для тех, кто ценит качество.", 5, "17:00-23:00", Range.VIP));

            offers.Add(new Offer("Ночной абонемент", "Для любителей тренироваться в тишине ночи мы предлагаем ночной абонемент. Пользуйтесь тренажерным залом с 23:00 до 6:00 по сниженной цене. " +
                "Идеальный вариант для тех, кто хочет избежать толпы и тренироваться в комфортных условиях.", 15, "23:00-06:00", Range.General));

            offers.Add(new Offer("Ночной VIP-доступ", "Наш VIP-ночной доступ — это уникальная возможность тренироваться в полной приватности. Включает персональные зоны, премиальное оборудование и услуги " +
                "персонального тренера. Также доступны легкие закуски и напитки. Абонемент действует с 23:00 до 6:00.", 20, "23:00-06:00", Range.VIP));

            offers.Add(new Offer("Дневной абонемент", "Оптимальный выбор для тех, кто предпочитает тренироваться днем. Абонемент предоставляет доступ с 12:00 до 17:00 и включает все зоны зала, " +
                "групповые тренировки и бонусные консультации с тренером.", 10, "12:00-17:00", Range.General));

            offers.Add(new Offer("Дневной VIP-доступ", "Для тех, кто хочет максимизировать свои дневные тренировки, мы предлагаем VIP-дневной доступ. Включает премиальные тренажеры, персональные тренировки " +
                "и скидки на дополнительные услуги. Доступен с 12:00 до 17:00.", 18, "12:00-17:00", Range.VIP));

            offersList.ItemsSource = offers;
        }

        private void offersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                titleBlock.Inlines.Clear();
                Offer selectedOffer = e.AddedItems[0] as Offer;

                titleBlock.Text = $"{selectedOffer.Name} \n  ";
                descriptionBlock.Text = $"{selectedOffer.Description}\n";
                infoBlock.Text = $"Время: {selectedOffer.Time}\n Цена: {selectedOffer.Price}\n Тип: {selectedOffer.Range}";
            }
            else
            {
                titleBlock.Text = string.Empty;
            }
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            AdminAccount client = new AdminAccount(App.Phone_Number);
            client.Show();
            Hide();
        }

        private void Check_reviews(object sender, RoutedEventArgs e)
        {
            ReviewPage review = new ReviewPage();
            review.Show();
        }
    }
}
