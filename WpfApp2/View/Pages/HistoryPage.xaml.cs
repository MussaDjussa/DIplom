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
using WpfApp2.Model;

namespace WpfApp2.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        public List<ChartModel> list = new List<ChartModel>();
        public HistoryPage()
        {
            InitializeComponent();
            list.Add(new ChartModel() { Name = "CSGO", Hours = 200 });
            list.Add(new ChartModel() { Name = "DOTA", Hours = 2392 });
            list.Add(new ChartModel() { Name = "Wot", Hours = 839 });
            list.Add(new ChartModel() { Name = "Apex Legends", Hours = 124 });
            list.Add(new ChartModel() { Name = "Crysis", Hours = 53 });
            list.Add(new ChartModel() { Name = "GTA V", Hours = 1032 });
            list.Add(new ChartModel() { Name = "Clash Royal", Hours = 420 });

            ColumnSeries.ItemsSource = list;
        }
    }
}
