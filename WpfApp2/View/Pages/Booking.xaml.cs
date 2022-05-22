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
            Schedule.DisplayDate = DateTime.Now;
            Schedule.ResourceCollection = Resources;
            Schedule.MaximumDate = DateTime.Now.AddDays(30);
            Schedule.AppointmentEditorOpening += Schedule_AppointmentEditorOpening;
            Schedule.CellLongPressed += Schedule_CellLongPressed;
            Schedule.CellTapped += Schedule_CellTapped;
            Schedule.AppointmentTapped += Schedule_AppointmentTapped;
            //DatePickerEnd.DisplayDateEnd = DateTime.Now.AddDays(30);
            TimePickerStart.Is24Hours = true;
            TimePickerEnd.Is24Hours = true;

            this.Schedule.ItemsSource = list;

            Schedule.CellDoubleTapped += Schedule_CellDoubleTapped;
        }

        public int DayTimeBegin { get; set; }

        public int MonthTimeBegin { get; set; }

        public int HourTimeBegin { get; set; }

        public int MinuteTimeBegin { get; set; }

        public string TimeCollapseBegin { get; set; }

        public int DayTimeEnd { get; set; }

        public int MonthTimeEnd { get; set; }

        public int HourTimeEnd { get; set; }

        public int MinuteTimeEnd {get;set;}

        public string TimeCollapseEnd { get; set; }

        private void Schedule_AppointmentTapped(object sender, AppointmentTappedArgs e)
        {
            if(e.Appointment != null)
            {
                TempAppointment.StartTime = e.Appointment.StartTime;
                TempAppointment.EndTime = e.Appointment.EndTime;
                TempAppointment.Subject = e.Appointment.Subject;
                TempAppointment.Notes = e.Appointment.Notes;
                TempAppointment.AppointmentBackground = e.Appointment.AppointmentBackground;

                StartTimeDatePicker = e.Appointment.StartTime.Day.ToString();
                EndTimeDatePicker = e.Appointment.EndTime.Day.ToString();

                StartTimeTimePicker = e.Appointment.StartTime.TimeOfDay.ToString();
                EndTimeTimePicker = e.Appointment.EndTime.TimeOfDay.ToString();
            }
        }

        public DateTime CellTaped { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        private void Schedule_CellTapped(object sender, CellTappedEventArgs e)
        {
            //StartTimeDatePicker = e.DateTime;
            //EndTimeDatePicker = e.DateTime.Hour;
            CellTaped = e.DateTime;

            if (e.Appointments != null)
            {
                TempAppointment = e.Appointment;
            }
        }

        private void Schedule_CellLongPressed(object sender, CellLongPressedEventArgs e)
        {
            

            DatePickerStart.IsEnabled = false;
            DatePickerEnd.IsEnabled = false;
            if (e.Appointment != null)
            {
                var patternAppointment = RecurrenceHelper.GetPatternAppointment(this.Schedule, e.Appointment);
                TempAppointment = patternAppointment;
                DialogEditor.IsOpen = true;
                DatePickerStart.Value = patternAppointment.StartTime;
                DatePickerEnd.Value = patternAppointment.EndTime;
                Username.Text = patternAppointment.Subject;
                Description.Text = patternAppointment.Notes;
            }
        }

        public ScheduleAppointment TempAppointment { get; set; } = new ScheduleAppointment();


      

        private void Schedule_CellDoubleTapped(object sender, CellDoubleTappedEventArgs e)
        {
            DayTimeBegin = CellTaped.Day;
            HourTimeBegin = CellTaped.Hour;
            MonthTimeBegin = CellTaped.Month;
            MinuteTimeBegin = CellTaped.Minute;

            DialogEditEditButton.Visibility = Visibility.Collapsed;

            TimeCollapseBegin = $"{DateTime.Now.Year}.{MonthTimeBegin}.{DayTimeBegin}.{HourTimeBegin}.{MinuteTimeBegin}";

            DatePickerStart.IsEnabled = true;
            DatePickerEnd.IsEnabled = true;
            DialogEditSaveButton.Visibility = Visibility.Visible;
            Username.Text = "Musa"; // username

            DatePickerStart.Value = CellTaped;
            DatePickerEnd.Value = CellTaped;
            TimePickerStart.SelectedTime = CellTaped;
            TimePickerEnd.SelectedTime = CellTaped.AddHours(1);

            DialogEditor.IsOpen = true;

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
                StartTime = CellTaped,
                EndTime = CellTaped.AddHours(1),
                Subject = "Musa", // username
                Notes = Description.Text,
                AppointmentBackground = new SolidColorBrush(DialogEditorColorpicker.Color),
                StartTimeZone = "Russian Standard Time",
                EndTimeZone = "Russian Standard Time"
            });;
       
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
        
            foreach (var item in list.Where(q=>q.Id == TempAppointment.Id))
            {
                if(item != null)
                {
                    item.StartTime = DatePickerStart.Value ?? TempAppointment.StartTime;
                    item.EndTime = DatePickerEnd.Value ?? TempAppointment.StartTime;
                    item.AppointmentBackground = new SolidColorBrush(DialogEditorColorpicker.Color);
                    item.Notes = Description.Text ?? TempAppointment.Notes;
                    item.Subject = Username.Text ?? TempAppointment.Subject;
                    DialogEditor.IsOpen=false;
                }
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            DatePickerStart.IsEnabled = true;
            DatePickerEnd.IsEnabled = true;

            Username.Text = "Musa"; // username

            DatePickerStart.Value = CellTaped;
            DatePickerEnd.Value = CellTaped;
            TimePickerStart.SelectedTime = CellTaped;
            TimePickerEnd.SelectedTime = CellTaped;

            DialogEditor.IsOpen = true;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            DialogEditEditButton.Visibility=Visibility.Visible;
            DatePickerStart.IsEnabled = false;
            DatePickerEnd.IsEnabled= false;
            DialogEditor.IsOpen = true;
            DialogEditSaveButton.Visibility = Visibility.Collapsed;
            
            
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if(TempAppointment != null)
            {
                foreach (var item in list.Where(q => q.Id == TempAppointment.Id))
                {
                    if (item != null)
                    {
                        list.Remove(item);
                    }
                }
            }
            
        }

        public string StartTimeDatePicker { get; set; }
        public string StartTimeTimePicker { get; set; }
        public string EndTimeTimePicker { get; set; }
        public string EndTimeDatePicker { get; set; }

        private void DatePickerStart_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var value = DatePickerStart.Value;
            int valueDay = DateTime.Parse(DateTime.Now.AddDays(30).ToString()).Day;
            int valueMonth = DateTime.Parse(DateTime.Now.AddDays(30).ToString()).Month;
            if (DateTime.Parse(value.ToString()).Year < DateTime.Now.Year || DateTime.Parse(value.ToString()).Year > DateTime.Now.Year
                || DateTime.Parse(value.ToString()).Day < DateTime.Now.Day || DateTime.Now.Month < DateTime.Parse($"{DatePickerStart.Value}").Month
                || DateTime.Parse($"{DatePickerStart.Value}").Month < DateTime.Now.Month )
            {
                MessageBox.Show("Ошибка");
                DatePickerStart.Value = CellTaped;
            }
            else
            {
                StartTimeDatePicker = DatePickerStart.Value.ToString();

                DateTime day = DatePickerStart.Value ?? DateTime.Parse(TimeCollapseBegin);

                DayTimeBegin = day.Day;
                MonthTimeBegin = day.Month;
                HourTimeBegin = day.Hour;
                MinuteTimeBegin = day.Minute;
            }
        }

        private void TimePickerStart_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            StartTimeTimePicker = TimePickerStart.SelectedTime.ToString();
        }

        private void DatePickerEnd_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EndTimeDatePicker = DatePickerEnd.Value.ToString();
        }

        private void TimePickerEnd_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
             EndTimeTimePicker = TimePickerEnd.SelectedTime.ToString();
        }
    }
}
