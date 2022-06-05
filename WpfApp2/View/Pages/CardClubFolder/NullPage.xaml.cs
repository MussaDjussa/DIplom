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

namespace WpfApp2.View.Pages.CardClubPage
{
    /// <summary>
    /// Логика взаимодействия для NullPage.xaml
    /// </summary>
    public partial class NullPage : Page
    {
        /// <summary>
        /// тут будет логика подзагрузки контента о клубной карте, изначально по умолчанию 
        /// у всех пользователей будет неактивная карта
        /// </summary>
        public NullPage()
        {
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new InformationCardClubPage());
        }
    }
}
