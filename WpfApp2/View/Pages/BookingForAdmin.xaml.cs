using Syncfusion.UI.Xaml.Scheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Threading;
using WpfApp2.ViewModel;

namespace WpfApp2.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для BookingForAdmin.xaml
    /// </summary>
    public partial class BookingForAdmin : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        DispatcherTimer time = new DispatcherTimer();
        public BookingForAdmin()
        {
            InitializeComponent();
            time.Interval = TimeSpan.FromSeconds(5);
            time.Tick += Time_Tick;
            Schedule.MinimumDate = DateTime.Now;
            Schedule.ResourceCollection = Resources;

            //DataContext = App.scheduleViewModel;
        }

        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private void Time_Tick(object sender, EventArgs e)
        {
            Schedule.MinimumDate = DateTime.Now;
        }

        private void Day_Click(object sender, RoutedEventArgs e)
        {
            Schedule.ViewType = Syncfusion.UI.Xaml.Scheduler.SchedulerViewType.TimelineDay;
        }

        private void Week_Click(object sender, RoutedEventArgs e)
        {
            Schedule.ViewType = Syncfusion.UI.Xaml.Scheduler.SchedulerViewType.Week;
        }

        private void Month_Click(object sender, RoutedEventArgs e)
        {
            Schedule.ViewType = Syncfusion.UI.Xaml.Scheduler.SchedulerViewType.Month;
        }

        private void Schedule_AppointmentEditorClosing(object sender, Syncfusion.UI.Xaml.Scheduler.AppointmentEditorClosingEventArgs e)
        {
            var appointment = e.Appointment as ScheduleAppointment;
            if (appointment != null)
            {
                if (appointment.StartTime.Day == DateTime.Now.Day)
                    e.Handled = true;
            }
        }

        private void TimeConfigure_Click(object sender, RoutedEventArgs e)
        {
            StartEndConfig.IsOpen = true;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            StartEndConfig.IsOpen = false;
        }

        private void StartTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(Regex.IsMatch(StartTime.Text, "[^0-9]"))
            {
                StartTime.Text = StartTime.Text.Remove(StartTime.Text.Length - 1);
                StartTime.SelectionStart = StartTime.Text.Length;
            }
        }

        private void EndTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(EndTime.Text, "[^0-9]"))
            {
                EndTime.Text = StartTime.Text.Remove(EndTime.Text.Length - 1);
                EndTime.SelectionStart = EndTime.Text.Length;
            }
        }

        private void ConfigureTime_Click(object sender, RoutedEventArgs e)
        {
            if(int.Parse(StartTime.Text) > 24 ||
               int.Parse(StartTime.Text) < 0  ||
               int.Parse(EndTime.Text) > 24   || 
               int.Parse(EndTime.Text) < 0    ||
               int.Parse(EndTime.Text) < int.Parse(StartTime.Text))
            {
                MessageBox.Show("Ошибка");
            }
            else
            {
                MessageBox.Show("ок");
            }

        }
    }
    
}
