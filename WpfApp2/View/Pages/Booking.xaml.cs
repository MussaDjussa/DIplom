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
            TimePickerStart.Is24Hours = true;
            TimePickerEnd.Is24Hours = true;
            Schedule.MinimumDate = DateTime.Now;
            this.Schedule.ItemsSource = list;
            Schedule.CellDoubleTapped += Schedule_CellDoubleTapped;
        }

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

        public int MinuteTimeEnd {get;set;}

        public string TimeCollapseEnd { get; set; }

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
                TimePickerEnd.SelectedTime = e.Appointment.EndTime;
                TimePickerStart.SelectedTime = e.Appointment.StartTime;

                StartTime = e.Appointment.StartTime;
                EndTime=e.Appointment.EndTime;
            }
        }


        public DateTime CellTaped { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        private void Schedule_CellTapped(object sender, CellTappedEventArgs e)
        {
            Schedule.MinimumDate = App.DateTimeNow;
            CellTaped = e.DateTime;

            if (e.Appointments != null)
            {
                TempAppointment = e.Appointment;
            }
        }


        public ScheduleAppointment TempAppointment { get; set; } = new ScheduleAppointment();

        private void Schedule_CellDoubleTapped(object sender, CellDoubleTappedEventArgs e)
        {
            DialogEditorColorpicker.Color = (Color)ColorConverter.ConvertFromString("#FF8168E7");

            Schedule.MinimumDate = App.DateTimeNow;
            DayTimeBegin = CellTaped.Day;
            HourTimeBegin = CellTaped.Hour;
            MonthTimeBegin = CellTaped.Month;
            MinuteTimeBegin = CellTaped.Minute;

            HourTimeEnd = CellTaped.Hour+1;


            DialogEditEditButton.Visibility = Visibility.Collapsed;

            TimeCollapseBegin = $"{DateTime.Now.Year}.{MonthTimeBegin}.{DayTimeBegin} {HourTimeBegin}:{MinuteTimeBegin}:{0}";

            TimeCollapseEnd = $"{DateTime.Now.Year}.{MonthTimeBegin}.{DayTimeBegin} {HourTimeEnd}:{MinuteTimeEnd}:{0}";

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

        public DateTime _minDate = DateTime.Now;

        public DateTime MinTime
        {
            get => Schedule.MinimumDate = _minDate;

            set
            {
                Schedule.MinimumDate = DateTime.Now;
                OnPropertyChanged("MinTime");
            }
        }
        

        private void Time_Tick(object sender, EventArgs e)
        {
            OnPropertyChanged("MinTime");
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
        private void DialogEditSaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogEditor.IsOpen = false;
            ScheduleAppointment appointment = new ScheduleAppointment()
            {
                StartTime = DateTime.Parse($"{TimeCollapseBegin}"),
                EndTime = DateTime.Parse($"{TimeCollapseEnd}"),
                Subject = App.bookingModel.Username, // username
                Notes = Description.Text,
                AppointmentBackground = new SolidColorBrush(DialogEditorColorpicker.Color),
                StartTimeZone = "Russian Standard Time",
                EndTimeZone = "Russian Standard Time"
            };

             list.Add(appointment);

            Description.Text = "";
            Username.Text = "";

            Appointment appointment1 = new Appointment() {

                AppointmentCode = (int)appointment.Id,
                Background = Convert.ToString( appointment.AppointmentBackground),
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

        private void DialogEditEditButton_Click(object sender, RoutedEventArgs e)
        {
            Schedule.MinimumDate = App.DateTimeNow;
            foreach (var item in list.Where(q=>q.Id == Kakayatahyinya.Id))
            {
                if(item != null)
                {
                    item.StartTime = FullTimeInit_Start;
                    item.EndTime = FullTimeInit_End;
                    item.AppointmentBackground = new SolidColorBrush(DialogEditorColorpicker.Color);
                    item.Notes = Description.Text ?? TempAppointment.Notes;
                    item.Subject = Username.Text ?? TempAppointment.Subject;
                    DialogEditor.IsOpen=false;
                }
            }
        }
        

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            DialogEditEditButton.Visibility=Visibility.Visible;
            DatePickerStart.IsEnabled = false;
            DatePickerEnd.IsEnabled= false;
            DialogEditor.IsOpen = true;
            DialogEditSaveButton.Visibility = Visibility.Collapsed;
             
            if(TempAppointment != null )
            {
                
                DialogEditor.IsOpen = true;
                Username.Text = Kakayatahyinya.Subject;
                Description.Text = Kakayatahyinya.Notes;
            }
            
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

        public DateTime FullTimeInit_Start { get; set; }
        public DateTime FullTimeInit_End { get; set; }

        private void DatePickerStart_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Schedule.MinimumDate = App.DateTimeNow;
            var value = DatePickerStart.Value;
            if (DateTime.Parse(value.ToString()).Year < DateTime.Now.Year || DateTime.Parse(value.ToString()).Year > DateTime.Now.Year
                || DateTime.Parse(value.ToString()).Day < DateTime.Now.Day || DateTime.Now.Month > DateTime.Parse($"{DatePickerStart.Value}").Month
                || DateTime.Parse($"{DatePickerStart.Value}").Month > DateTime.Now.AddDays(30).Month)
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

                TimeCollapseBegin = $"{DateTime.Now.Year}-{MonthTimeBegin}-{DayTimeBegin} {HourTimeBegin}:{MinuteTimeBegin}:{0}";

                DatePickerStart.Value = DateTime.Parse(TimeCollapseBegin);
            }
        }

        private void TimePickerStart_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            Schedule.MinimumDate = App.DateTimeNow;
            if (TimePickerStart.SelectedTime < Schedule.MinimumDate)
            {
                TimePickerStart.SelectedTime = Schedule.MinimumDate.AddMinutes(10);
            }
            else
            {
                DateTime day = TimePickerStart.SelectedTime ?? DateTime.Parse(TimeCollapseBegin);

                DayTimeBegin = day.Day;
                MonthTimeBegin = day.Month;
                HourTimeBegin = day.Hour;
                MinuteTimeBegin = day.Minute;

                TimeCollapseBegin = $"{DateTime.Now.Year}-{MonthTimeBegin}-{DayTimeBegin} {HourTimeBegin}:{MinuteTimeBegin}:{0}";
                TimePickerStart.SelectedTime = DateTime.Parse(TimeCollapseBegin);
            }

            
        }

        private void DatePickerEnd_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var value = DatePickerEnd.Value;
            if ((DateTime.Parse(value.ToString()).Year < DateTime.Now.Year || DateTime.Parse(value.ToString()).Year > DateTime.Now.Year
                || DateTime.Parse(value.ToString()).Day < DateTime.Now.Day || DateTime.Now.Month > DateTime.Parse($"{DatePickerEnd.Value}").Month
                || DateTime.Parse($"{DatePickerEnd.Value}").Month > DateTime.Now.AddDays(30).Month))
            {
                MessageBox.Show("Ошибка");
                DatePickerEnd.Value = CellTaped;
            }
            else
            {
                Schedule.MinimumDate = App.DateTimeNow;

                DayTimeEnd = DateTime.Parse($"{DatePickerEnd.Value}").Day;
                MonthTimeEnd = DateTime.Parse($"{DatePickerEnd.Value}").Month;
                HourTimeEnd = DateTime.Parse($"{TimeCollapseEnd}").Hour;
                MinuteTimeEnd = DateTime.Parse($"{TimeCollapseEnd}").Minute;

                TimeCollapseEnd = $"{DateTime.Now.Year}-{MonthTimeEnd}-{DayTimeEnd} {HourTimeEnd}:{MinuteTimeEnd}:{0}";

   
            }
        }

        private void TimePickerEnd_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            Schedule.MinimumDate = App.DateTimeNow;
            //if (TimePickerEnd.SelectedTime < Schedule.MinimumDate
            //    || TimePickerEnd.SelectedTime <= DateTime.Now
            //   )
            //{
            //    TimePickerEnd.SelectedTime = CellTaped.AddHours(1);

            //    if (TimePickerStart.SelectedTime == DateTime.Parse($"{DateTime.Now.Year}-{CellTaped.Month}-{CellTaped.Day} {23}:{0}:{0}")
            //        && TimePickerEnd.SelectedTime == DateTime.Parse($"{DateTime.Now.Year}-{CellTaped.Month}-{CellTaped.Day} {0}:{0}:{0}"))
            //    {
            //        TimeCollapseBegin = $"{DateTime.Now.Year}-{CellTaped.Month}-{CellTaped.Day} {23}:{0}:{0}";
            //        TimeCollapseEnd = $"{DateTime.Now.Year}-{CellTaped.Month}-{CellTaped.Day} {23}:{59}:{59}";

            //        TimePickerStart.SelectedTime = DateTime.Parse(TimeCollapseBegin);
            //        TimePickerEnd.SelectedTime = DateTime.Parse(TimeCollapseEnd);
            //    }
            //}
            //else
            //{

                Schedule.MinimumDate = App.DateTimeNow;
            //DateTime day = TimePickerEnd.SelectedTime ?? DateTime.Parse(TimeCollapseEnd);

            //DayTimeEnd = day.Day;
            //MonthTimeEnd = day.Month;
            //HourTimeEnd = day.Hour;
            //MinuteTimeEnd = day.Minute;


            DayTimeEnd = DateTime.Parse($"{TimeCollapseEnd}").Day;
            MonthTimeEnd = DateTime.Parse($"{TimeCollapseEnd}").Month;
            HourTimeEnd = TimePickerEnd.SelectedTime.Value.Hour;
            MinuteTimeEnd = TimePickerEnd.SelectedTime.Value.Minute;

            TimeCollapseEnd = $"{DateTime.Now.Year}-{MonthTimeEnd}-{DayTimeEnd} {HourTimeEnd}:{MinuteTimeEnd}:{0}";


            }
        //}
    }
}
