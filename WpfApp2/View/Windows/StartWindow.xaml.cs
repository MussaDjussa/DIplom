using System.Windows;
using WpfApp2.View.Pages;

namespace WpfApp2.View.Windows
{
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
            NavigationServices.Content = new LoginPage();
        }
    }
}