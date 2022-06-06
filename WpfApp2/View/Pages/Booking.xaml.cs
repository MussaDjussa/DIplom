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


        ScheduleAppointmentCollection list = new ScheduleAppointmentCollection();

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public Booking()
        {
            InitializeComponent();
            Schedule.MinimumDate = DateTime.Now;
            Schedule.DisplayDate = DateTime.Now;
            Schedule.ResourceCollection = Resources;
            Schedule.MaximumDate = DateTime.Now.AddDays(30);
            Schedule.AppointmentEditorOpening += Schedule_AppointmentEditorOpening;
            Schedule.CellTapped += Schedule_CellTapped;
            Schedule.AppointmentTapped += Schedule_AppointmentTapped;
            Schedule.AppointmentDragStarting += Schedule_AppointmentDragStarting;
            //TimePickerStart.Is24Hours = true;
            //TimePickerEnd.Is24Hours = true;
            Schedule.MinimumDate = DateTime.Now;
            this.Schedule.ItemsSource = list;
            Schedule.CellDoubleTapped += Schedule_CellDoubleTapped;

            DatePickerStart.Value = DateTime.Now;
            TimePickerStart.Value = DateTime.Now;
            DatePickerEnd.Value = DateTime.Now;
            TimePickerEnd.Value = DateTime.Now;

            TimePickerEnd.MinTime = TimeSpan.Parse("10:00:00");
        }
        public DateTime DateTimePicker { get; set; }
        private void Schedule_AppointmentDragStarting(object sender, AppointmentDragStartingEventArgs e)
        {
            e.Cancel = true;
        }

        public int DayTimeBegin { get; set; }

        public int MonthTimeBegin { get; set; }

        public int HourTimeBegin { get; set; }

        public int MinuteTimeBegin { get; set; }

        public string TimeCollapseBegin { get; set; }

        public int DayTimeEnd { get; set; }

        public int MonthTimeEnd { get; set; }

        public int HourTimeEnd { get; set; }

        public int MinuteTimeEnd { get; set; }

        public string TimeCollapseEnd { get; set; }
        public string TimeCollapseEnd1 { get; set; }

        public ScheduleAppointment Kakayatahyinya { get; set; } = new ScheduleAppointment();

        private void Schedule_AppointmentTapped(object sender, AppointmentTappedArgs e)
        {
            Schedule.MinimumDate = App.DateTimeNow;
            if (e.Appointment != null)
            {
                Kakayatahyinya.Id = e.Appointment.Id;
                StartTime = e.Appointment.StartTime;
                EndTime = e.Appointment.EndTime;

                Kakayatahyinya.Notes = e.Appointment.Notes;
                Kakayatahyinya.Subject = e.Appointment.Subject;


                DatePickerStart.Value = e.Appointment.StartTime;
                DatePickerEnd.Value = e.Appointment.EndTime;
                TimePickerEnd.Value = e.Appointment.EndTime;
                TimePickerStart.Value = e.Appointment.StartTime;

                StartTime = e.Appointment.StartTime;
                EndTime = e.Appointment.EndTime;
            }
        }


        public DateTime CellTaped { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        private void Schedule_CellTapped(object sender, CellTappedEventArgs e)
        {
            Schedule.MinimumDate = App.DateTimeNow;
            CellTaped = e.DateTime;
            TimeCollapseBegin = $"{DateTime.Now.Year}.{e.DateTime.Month}.{e.DateTime.Day} {e.DateTime.Hour}:{e.DateTime.Minute}:{0}";
            TimeCollapseEnd = $"{DateTime.Now.Year}.{e.DateTime.Month}.{e.DateTime.Day} {e.DateTime.Hour+1}:{e.DateTime.Minute}:{0}";

            DatePickerStart.Value = e.DateTime;
            DatePickerEnd.Value = e.DateTime;
            TimePickerStart.Value = e.DateTime;
            TimePickerEnd.Value = e.DateTime.AddHours(1);

            if (e.Appointments != null)
            {
                TempAppointment = e.Appointment;
            }
        }


        public ScheduleAppointment TempAppointment { get; set; } = new ScheduleAppointment();

        private void Schedule_CellDoubleTapped(object sender, CellDoubleTappedEventArgs e)
        {
            Schedule.MinimumDate = App.DateTimeNow;

            DatePickerStart.Value = CellTaped;
            TimePickerStart.Value = CellTaped;
            DatePickerEnd.Value = CellTaped;
            TimePickerEnd.Value = DateTime.Parse(CellTaped.ToString()).AddHours(1);

            DialogEditorColorpicker.Color = (Color)ColorConverter.ConvertFromString("#FF8168E7");

            Schedule.MinimumDate = App.DateTimeNow;
            DayTimeBegin = CellTaped.Day;
            HourTimeBegin = CellTaped.Hour;
            MonthTimeBegin = CellTaped.Month;
            MinuteTimeBegin = CellTaped.Minute;

            HourTimeEnd = CellTaped.Hour /*+ 1*/;


            DialogEditEditButton.Visibility = Visibility.Collapsed;

            //TimeCollapseBegin = $"{DateTime.Now.Year}.{MonthTimeBegin}.{DayTimeBegin} {HourTimeBegin}:{MinuteTimeBegin}:{0}";

            //TimeCollapseEnd = $"{DateTime.Now.Year}.{MonthTimeBegin}.{DayTimeBegin} {CellTaped.Hour + 1}:{MinuteTimeEnd}:{0}";

            DialogEditSaveButton.Visibility = Visibility.Visible;
            Username.Text = "Musa"; // username
            DatePickerStart.Value = CellTaped;
            DatePickerEnd.Value = CellTaped;
            TimePickerStart.Value = CellTaped;
            TimePickerEnd.Value = CellTaped.AddHours(1);

            DialogEditor.IsOpen = true;

            if(TimePickerStart.Value.Value.Hour == 23)
            {
                DatePickerEnd.Value = new DateTime(TimePickerStart.Value.Value.Year, TimePickerStart.Value.Value.Month, TimePickerStart.Value.Value.Day + 1,
                  0, TimePickerStart.Value.Value.Minute, TimePickerStart.Value.Value.Second);
            }

        }
        private void Schedule_AppointmentEditorOpening(object sender, AppointmentEditorOpeningEventArgs e)
        {
            
            Schedule.MinimumDate = App.DateTimeNow;
            e.Cancel = true;
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
        public static ScheduleAppointment appointment;
        private void DialogEditSaveButton_Click(object sender, RoutedEventArgs e)
        {
            Schedule.MinimumDate = App.DateTimeNow;
            try
            {
                TimeCollapseBegin = $"{DateTime.Now.Year}-{DatePickerStart.Value.Value.Month}-{DatePickerStart.Value.Value.Day} {TimePickerStart.Value.Value.Hour}:{TimePickerStart.Value.Value.Minute}:0";

                TimeCollapseEnd = $"{DateTime.Now.Year}-{DatePickerEnd.Value.Value.Month}-{DatePickerEnd.Value.Value.Day} {TimePickerEnd.Value.Value.Hour}:{TimePickerEnd.Value.Value.Minute}:0";

                appointment = new ScheduleAppointment()
                {
                    StartTime = DateTime.Parse($"{TimeCollapseBegin}"),
                    EndTime = DateTime.Parse($"{TimeCollapseEnd}"),
                    Subject = App.bookingModel.Username, // username
                    Notes = Description.Text,
                    AppointmentBackground = new SolidColorBrush(DialogEditorColorpicker.Color),
                    StartTimeZone = "Russian Standard Time",
                    EndTimeZone = "Russian Standard Time"
                };


                if (TimePickerStart.Value.Value.Hour == 23 && TimePickerEnd.Value.Value.Hour == 0)
                {
                    appointment.EndTime = DateTime.Parse($"{TimeCollapseEnd1}");
                }

                DialogEditor.IsOpen = false;



                list.Add(appointment);

                Description.Text = "";
                Username.Text = "";

                Appointment appointment1 = new Appointment()
                {

                    AppointmentCode = (int)appointment.Id,
                    Background = Convert.ToString(appointment.AppointmentBackground),
                    Description = appointment.Notes,
                    UserId = 1,
                    StartTime = appointment.StartTime,
                    EndTime = appointment.EndTime,
                    Title = appointment.Subject

                };


                var response = App.httpClient.PostAsJsonAsync("appointment", appointment1).Result;


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("ok");
                }
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("bad request");
                }

            }
            catch (FormatException)
            {
                TimeCollapseEnd = $"{DateTime.Now.Year}-{DatePickerEnd.Value.Value.Month}-{DatePickerEnd.Value.Value.Day} {TimePickerEnd.Value.Value.Hour}:{TimePickerEnd.Value.Value.Minute}:0";
               


                appointment = new ScheduleAppointment()
                {
                    StartTime = DateTime.Parse($"{TimeCollapseBegin}"),
                    EndTime = DateTime.Parse($"{TimeCollapseEnd}"),
                    Subject = App.bookingModel.Username, // username
                    Notes = Description.Text,
                    AppointmentBackground = new SolidColorBrush(DialogEditorColorpicker.Color),
                    StartTimeZone = "Russian Standard Time",
                    EndTimeZone = "Russian Standard Time"
                };

                if (TimePickerStart.Value.Value.Hour == 23 && TimePickerEnd.Value.Value.Hour == 0)
                {
                    appointment.EndTime = DateTime.Parse($"{TimeCollapseEnd1}");
                }

                DialogEditor.IsOpen = false;



                list.Add(appointment);

                Description.Text = "";
                Username.Text = "";

                Appointment appointment1 = new Appointment()
                {

                    AppointmentCode = (int)appointment.Id,
                    Background = Convert.ToString(appointment.AppointmentBackground),
                    Description = appointment.Notes,
                    UserId = 1,
                    StartTime = appointment.StartTime,
                    EndTime = appointment.EndTime,
                    Title = appointment.Subject

                };


                var response = App.httpClient.PostAsJsonAsync("appointment", appointment1).Result;


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("ok");
                }
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("bad request");
                }
            }
        }

        private void DialogEditEditButton_Click(object sender, RoutedEventArgs e)
        {
            Schedule.MinimumDate = App.DateTimeNow;
            foreach (var item in list.Where(q => q.Id == Kakayatahyinya.Id))
            {
                if (item != null)
                {
                    item.StartTime = FullTimeInit_Start;
                    item.EndTime = FullTimeInit_End;
                    item.AppointmentBackground = new SolidColorBrush(DialogEditorColorpicker.Color);
                    item.Notes = Description.Text ?? TempAppointment.Notes;
                    item.Subject = Username.Text ?? TempAppointment.Subject;
                    DialogEditor.IsOpen = false;
                }
            }
        }


        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Schedule.MinimumDate = App.DateTimeNow;
            DialogEditEditButton.Visibility = Visibility.Visible;
            DatePickerStart.IsEnabled = false;
            DatePickerEnd.IsEnabled = false;
            DialogEditor.IsOpen = true;
            DialogEditSaveButton.Visibility = Visibility.Collapsed;

            if (TempAppointment != null)
            {

                DialogEditor.IsOpen = true;
                Username.Text = Kakayatahyinya.Subject;
                Description.Text = Kakayatahyinya.Notes;
            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Schedule.MinimumDate = App.DateTimeNow;
            if (TempAppointment != null)
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

        public DateTime FullTimeInit_Start { get; set; }
        public DateTime FullTimeInit_End { get; set; }

        private void DatePickerStart_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Schedule.MinimumDate = App.DateTimeNow;
            if (DatePickerStart.Value.Value.Year < DateTime.Now.Year || DatePickerStart.Value.Value.Year > DateTime.Now.Year
                || DatePickerStart.Value.Value.Day < DateTime.Now.Day || DateTime.Now.Month > DatePickerStart.Value.Value.Month
                || DatePickerStart.Value.Value.Month > DatePickerStart.Value.Value.AddDays(30).Month)
            {
                MessageBox.Show("Ошибка");
                DatePickerStart.Value = CellTaped;
            }
            else
            {

                DateTime day = DatePickerStart.Value ?? DateTime.Parse(TimeCollapseBegin);

                DayTimeBegin = day.Day;
                MonthTimeBegin = day.Month;
                HourTimeBegin = day.Hour;
                MinuteTimeBegin = day.Minute;

                TimeCollapseBegin = $"{DateTime.Now.Year}-{DatePickerStart.Value.Value.Month}-{DatePickerStart.Value.Value.Day} {DatePickerStart.Value.Value.Hour}:{DatePickerStart.Value.Value.Minute}:{0}";

                DatePickerStart.Value = DateTime.Parse(TimeCollapseBegin);
                DatePickerEnd.MinDate = DateTime.Parse(DatePickerStart.Value.ToString());
            }
        }


        private void DatePickerEnd_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Schedule.MinimumDate = App.DateTimeNow;


            //DayTimeEnd = DateTime.Parse($"{DatePickerEnd.Value}").Day;
            //MonthTimeEnd = DateTime.Parse($"{DatePickerEnd.Value}").Month;
            //HourTimeEnd = TimeSpan.Parse($"{DateTime.Parse($"{TimeCollapseEnd}").Hour}").Hours;
            //MinuteTimeEnd = TimeSpan.Parse($"{DateTime.Parse($"{TimeCollapseEnd}").Minute}").Minutes;

            TimeCollapseEnd = $"{DateTime.Now.Year}-{DatePickerEnd.Value.Value.Month}-{DatePickerEnd.Value.Value.Day} {DatePickerEnd.Value.Value.Hour}:{DatePickerEnd.Value.Value.Minute}:{0}";


        }


        private void TimePickerStart_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Schedule.MinimumDate = App.DateTimeNow;
          
            if (TimePickerEnd != null)
                TimePickerEnd.MinTime = new TimeSpan(TimePickerStart.Value.Value.Hour + 1, TimePickerStart.Value.Value.Minute, TimePickerStart.Value.Value.Second);

            if (TimePickerEnd != null && TimePickerStart.Value.Value.Hour != 23)
                TimePickerEnd.Value = new DateTime(TimePickerStart.Value.Value.Year, TimePickerStart.Value.Value.Month, TimePickerStart.Value.Value.Day,
                    TimePickerStart.Value.Value.Hour + 1, TimePickerStart.Value.Value.Minute, TimePickerStart.Value.Value.Second);

            if (TimePickerStart.Value.Value.Hour == 23 && TimePickerEnd != null)
            {
                DatePickerEnd.Value = new DateTime(TimePickerStart.Value.Value.Year, TimePickerStart.Value.Value.Month, TimePickerStart.Value.Value.Day + 1,
                  0, TimePickerStart.Value.Value.Minute, TimePickerStart.Value.Value.Second);

                TimeCollapseEnd1 = DatePickerEnd.Value.ToString();
            }

            if (TimePickerStart != null)
            {
                HourTimeBegin = TimePickerStart.Value.Value.Hour;
                MinuteTimeBegin = TimePickerStart.Value.Value.Minute;
            }
                
        }

        private void TimePickerEnd_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(TimePickerEnd != null)
            {
                HourTimeEnd = TimePickerEnd.Value.Value.Hour;
                MinuteTimeEnd = TimePickerEnd.Value.Value.Minute;
            }
            Schedule.MinimumDate = App.DateTimeNow;
          
        }
    }
}
