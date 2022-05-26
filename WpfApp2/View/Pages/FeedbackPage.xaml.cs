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
    /// Логика взаимодействия для FeedbackPage.xaml
    /// </summary>
    public partial class FeedbackPage : Page
    {
        List<FeedBackModel> list = new List<FeedBackModel>();
        public FeedbackPage()
        {
            InitializeComponent();

            for (int i = 0; i < 30; i++)
            {
                list.Add(new FeedBackModel() { DatePost = DateTime.Now.ToLongDateString(),  Description = $"Description {i}" , Login = $"Login {i}"});
            }

            feedbackOther.ItemsSource = list;
            feedbackOwn.ItemsSource = list;
        }
    }
}
