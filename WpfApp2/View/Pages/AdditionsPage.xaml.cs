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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2.View.Pages.AdditionPage;

namespace WpfApp2.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdditionsPage.xaml
    /// </summary>
    public partial class AdditionsPage : Page
    {
        public AdditionsPage()
        {
            InitializeComponent();
        }

        ControllerPage controllerPage = new ControllerPage();
        DrinkPage drinkPage = new DrinkPage();
        FoodPage foodPage = new FoodPage();
        MainPage mainPage = new MainPage(); 

        private void Food_Checked(object sender, RoutedEventArgs e)
        {
            FoodIcon.Foreground = Brushes.DarkRed;
        }

        private void Drinkables_Checked(object sender, RoutedEventArgs e)
        {
            DrinkIcon.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3d54fc"));
        }

        private void Controllers_Checked(object sender, RoutedEventArgs e)
        {
            ControllerIcon.Foreground = Brushes.Purple;
        }

        private void Food_LostFocus(object sender, RoutedEventArgs e)
        {
            FoodIcon.Foreground = Brushes.Gray;
        }

        private void Drinkables_LostFocus(object sender, RoutedEventArgs e)
        {
            DrinkIcon.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#808080"));
        }

        private void Controllers_LostFocus(object sender, RoutedEventArgs e)
        {
            ControllerIcon.Foreground = Brushes.Gray;
        }

        private void All_Checked(object sender, RoutedEventArgs e)
        {
            AllIcon.Foreground = Brushes.DarkGreen;
        }

        private void All_LostFocus(object sender, RoutedEventArgs e)
        {
            AllIcon.Foreground = Brushes.Gray;
        }
    }
}
