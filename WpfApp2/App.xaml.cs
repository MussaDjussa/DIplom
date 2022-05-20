using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp2.Model;
using WpfApp2.View.Pages;
using WpfApp2.ViewModel;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        //public static CompClubDBEntities db = new CompClubDBEntities();

        public static HttpClient httpClient = new HttpClient();

        public static ScheduleViewModel scheduleViewModel = new ScheduleViewModel();
        public App()
        {
            App.httpClient.BaseAddress = new Uri("https://localhost:5001/api/");
            App.httpClient.DefaultRequestHeaders.Accept.Clear();
            App.httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
        }

        public static User users = new User();
    }
}
