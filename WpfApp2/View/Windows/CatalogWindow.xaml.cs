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
using WpfApp2.View.Pages;

namespace WpfApp2.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        MainPage mainPage = new MainPage();
        Booking booking = new Booking();
        MapPage mapPage = new MapPage();
        FeedbackPage feedbackPage = new FeedbackPage();
        HistoryPage historyPage = new HistoryPage();
        public CatalogWindow()
        {
            InitializeComponent();
            fContainer.Content = mainPage;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            fContainer.NavigationService.Navigate(mainPage);
            HomeIcon.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void btnHome_LostFocus(object sender, RoutedEventArgs e)
        {
            HomeIcon.Foreground = new SolidColorBrush(Colors.Gray);
        }

        private void Contact_Click(object sender, RoutedEventArgs e)
        {
            ContactIcon.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void Contact_LostFocus(object sender, RoutedEventArgs e)
        {
            ContactIcon.Foreground = new SolidColorBrush(Colors.Gray);
        }

        private void FeedBack_Click(object sender, RoutedEventArgs e)
        {
            fContainer.NavigationService.Navigate(feedbackPage);
            FeedBackIcon.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void FeedBack_LostFocus(object sender, RoutedEventArgs e)
        {
            FeedBackIcon.Foreground= new SolidColorBrush(Colors.Gray);
        }

        private void Card_Click(object sender, RoutedEventArgs e)
        {
            CardIcon.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void Card_LostFocus(object sender, RoutedEventArgs e)
        {
            CardIcon.Foreground= new SolidColorBrush(Colors.Gray);
        }

        private void Clock_Click(object sender, RoutedEventArgs e)
        {
            fContainer.NavigationService.Navigate(historyPage);
            ClockIcon.Foreground  = new SolidColorBrush(Colors.Red);
        }

        private void Clock_LostFocus(object sender, RoutedEventArgs e)
        {
            ClockIcon.Foreground= new SolidColorBrush(Colors.Gray);
        }

        private void Food_Click(object sender, RoutedEventArgs e)
        {
            FoodIcon.Foreground = new SolidColorBrush (Colors.Red);    
        }

        private void Food_LostFocus(object sender, RoutedEventArgs e)
        {
            FoodIcon.Foreground = new SolidColorBrush(Colors.Gray);
        }

        private void Book_Click(object sender, RoutedEventArgs e)
        {
            BookIcon.Foreground = new SolidColorBrush (Colors.Red);
            fContainer.NavigationService.Navigate(booking);
        }

        private void Book_LostFocus(object sender, RoutedEventArgs e)
        {
            BookIcon.Foreground= new SolidColorBrush (Colors.Gray);
        }

        private void Map_Click(object sender, RoutedEventArgs e)
        {
            fContainer.NavigationService.Navigate(mapPage);
            MapIcon.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void Map_LostFocus(object sender, RoutedEventArgs e)
        {
            MapIcon.Foreground = new SolidColorBrush(Colors.Gray);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ExitAccount_Click(object sender, RoutedEventArgs e)
        {

            new StartWindow().Show();
            this.Close();
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

    }
}
