using Syncfusion.UI.Xaml.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
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
using System.Windows.Threading;
using WpfApp2.Model;
using WpfApp2.ViewModel;

namespace WpfApp2.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для Booking.xaml
    /// </summary>
    public partial class Booking : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        DispatcherTimer time = new DispatcherTimer();

        ScheduleAppointmentCollection list = new ScheduleAppointmentCollection();
        public Booking()
        {
            InitializeComponent();
            time.Interval = TimeSpan.FromSeconds(5);
            time.Tick += Time_Tick;
            Schedule.MinimumDate = DateTime.Now;
            Schedule.ResourceCollection = Resources;
            Schedule.MaximumDate = DateTime.Now.AddDays(30);
            Schedule.AppointmentEditorOpening += Schedule_AppointmentEditorOpening;
            Schedule.AppointmentTapped += Schedule_AppointmentTapped;
            DatePickerStart.DisplayDateStart = DateTime.Now;
            //DatePickerEnd.DisplayDateEnd = DateTime.Now.AddDays(30);

            TimePickerStart.Is24Hours = true;
            TimePickerEnd.Is24Hours= true;
            
            this.Schedule.ItemsSource = list;

            Schedule.CellDoubleTapped += Schedule_CellDoubleTapped;
        }

        public ScheduleAppointment tempAppointment { get; set; } = new ScheduleAppointment();

        private void Schedule_AppointmentTapped(object sender, AppointmentTappedArgs e)
        {
          
            if (e.Appointment != null)
            {
                var patternAppointment = RecurrenceHelper.GetPatternAppointment(this.Schedule, e.Appointment);
                tempAppointment = patternAppointment;
                DialogEditor.IsOpen = true;

                DatePickerStart.SelectedDate = patternAppointment.StartTime;
                //DatePickerEnd.SelectedDate = patternAppointment.EndTime;
                Username.Text = patternAppointment.Subject;
                Description.Text = patternAppointment.Notes;
            }
        }

        public ScheduleAppointment OccureAppointment { get; set; } = new ScheduleAppointment();
        public Appointment SelectedAppointment { get; set; } = new Appointment();

        private void Schedule_CellDoubleTapped(object sender, CellDoubleTappedEventArgs e)
        {

            Username.Text = "Musa"; // username

            DatePickerStart.SelectedDate = e.DateTime;
            
            //DatePickerEnd.SelectedDate =e.DateTime.AddHours(1);
            TimePickerEnd.SelectedTime = e.DateTime.AddHours(1);

            TimePickerStart.SelectedTime = e.DateTime;
           
            DialogEditor.IsOpen = true;


            SelectedAppointment.StartTime = e.DateTime;
        }

        private void Schedule_AppointmentEditorOpening(object sender, AppointmentEditorOpeningEventArgs e)
        {
            e.Cancel = true;
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

        private void DialogEditorCancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogEditor.IsOpen = false;
        }

        private void DialogEditorCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            DatePickerStart.IsEnabled = false;
            DatePickerEnd.IsEnabled = false;
            TimePickerStart.IsEnabled = false;
            TimePickerEnd.IsEnabled = false;
        }

        private void DialogEditorCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            DatePickerStart.IsEnabled = true;
            DatePickerEnd.IsEnabled = true;
            TimePickerStart.IsEnabled = true;
            TimePickerEnd.IsEnabled = true;
        }

        private void DialogEditSaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogEditor.IsOpen = false;

            list.Add(new ScheduleAppointment()
            {
                StartTime = SelectedAppointment.StartTime,
                EndTime = SelectedAppointment.StartTime.AddHours(1),
                Subject = "Musa", // username
                Notes = Description.Text,
                AppointmentBackground = new SolidColorBrush(DialogEditorColorpicker.Color)
            });
       
            //var response = App.httpClient.PostAsJsonAsync("appointment", appointment).Result;

            //if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    MessageBox.Show("ok");
            //}
            //if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            //{
            //    MessageBox.Show("bad request");
            //}
        }

        private void DialogEditEditButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in list.Where(q=>q.Id == tempAppointment.Id))
            {
                if(item != null)
                {

                }
            }
        }
    }
}
