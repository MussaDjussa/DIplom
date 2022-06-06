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
    public partial class Booking : Page
    {

        /// <summary>
        /// update mindate in realtime
        /// </summary>
        public static DispatcherTimer time = new DispatcherTimer();

        /// <summary>
        /// appointment list
        /// </summary>
        ScheduleAppointmentCollection list = new ScheduleAppointmentCollection();

        public Booking()
        {
            InitializeComponent();
  
            Schedule.DisplayDate = DateTime.Now;
            Schedule.ResourceCollection = Resources;
            Schedule.MaximumDate = DateTime.Now.AddDays(30);
            Schedule.AppointmentEditorOpening += Schedule_AppointmentEditorOpening;
            Schedule.CellTapped += Schedule_CellTapped;
            Schedule.AppointmentTapped += Schedule_AppointmentTapped;
            Schedule.AppointmentDragStarting += Schedule_AppointmentDragStarting;

            this.Schedule.ItemsSource = list;
            Schedule.CellDoubleTapped += Schedule_CellDoubleTapped;

            DatePickerStart.Value = DateTime.Now;
            TimePickerStart.Value = DateTime.Now;
            DatePickerEnd.Value = DateTime.Now;
            TimePickerEnd.Value = DateTime.Now;

            TimePickerEnd.MinTime = TimeSpan.Parse("10:00:00");


            time.Interval = TimeSpan.FromMilliseconds(1);
            time.Tick += Time_Tick;
            time.Start();
        }
        /// <summary>
        /// event which notificate mindate for schedule.mindate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Time_Tick(object sender, EventArgs e)
        {
            Schedule.MinimumDate = DateTime.Now;

        }

        /// <summary>
        /// resize option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Schedule_AppointmentResizing(object sender, AppointmentResizingEventArgs e)
        {
            e.CanContinueResize = false;
        }

        /// <summary>
        /// we re unable gragndrop option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Schedule_AppointmentDragStarting(object sender, AppointmentDragStartingEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// for parsing datetime in string format
        /// </summary>
        public string TimeCollapseBegin { get; set; }
        /// <summary>
        /// for parsing datetime in string format
        /// </summary>
        public string TimeCollapseEnd { get; set; }
        /// <summary>
        /// for parsing datetime in string format
        /// </summary>
        public string TimeCollapseEnd1 { get; set; }

        /// <summary>
        /// temp variable for information appointment
        /// </summary>
        public Appointment AppointmentTapped { get; set; } = new Appointment();
        /// <summary>
        /// get full information about clicked appointment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Schedule_AppointmentTapped(object sender, AppointmentTappedArgs e)
        {
            if (e.Appointment != null)
            {
                AppointmentTapped.AppointmentCode = (int)e.Appointment.Id;
                AppointmentTapped.StartTime = e.Appointment.StartTime;
                AppointmentTapped.EndTime = e.Appointment.EndTime;
                AppointmentTapped.Background = e.Appointment.AppointmentBackground.ToString();
                AppointmentTapped.Note = e.Appointment.Notes;
                AppointmentTapped.Title = e.Appointment.Subject;
            }
        }

        /// <summary>
        /// this property allows information about clicked Cell
        /// </summary>
        public DateTime CellTaped { get; set; }

        /// <summary>
        /// celltaped Appointment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Schedule_CellTapped(object sender, CellTappedEventArgs e)
        {
            CellTaped = e.DateTime;
            TimeCollapseBegin = $"{DateTime.Now.Year}.{e.DateTime.Month}.{e.DateTime.Day} {e.DateTime.Hour}:{e.DateTime.Minute}:{0}";
            TimeCollapseEnd = $"{DateTime.Now.Year}.{e.DateTime.Month}.{e.DateTime.Day} {e.DateTime.Hour+1}:{e.DateTime.Minute}:{0}";

            DatePickerStart.Value = e.DateTime;
            DatePickerEnd.Value = e.DateTime;
            TimePickerStart.Value = e.DateTime;
            TimePickerEnd.Value = e.DateTime.AddHours(1);
        }

        /// <summary>
        /// double click on celltapped
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Schedule_CellDoubleTapped(object sender, CellDoubleTappedEventArgs e)
        {
            /// to open context meny
            DialogEditor.IsOpen = true;
            /// init color picker with default color
            DialogEditorColorpicker.Color = (Color)ColorConverter.ConvertFromString("#FF8168E7");
            /// collaspse edit button
            DialogEditEditButton.Visibility = Visibility.Collapsed;
            /// visible save button
            DialogEditSaveButton.Visibility = Visibility.Visible;
            /// inti username 
            Username.Text = "Musa"; // username
            /// init datepicker on celltaped
            DatePickerStart.Value = CellTaped;
            /// init datepicker on celltaped
            DatePickerEnd.Value = CellTaped;
            /// init timepicker on celltaped
            TimePickerStart.Value = CellTaped;
            /// init datepicker on celltaped, added default... 1 hours 
            TimePickerEnd.Value = CellTaped.AddHours(1);

           
            /// exception sutuatuion with 23 hours
            if(TimePickerStart.Value.Value.Hour == 23)
            {
                DatePickerEnd.Value = new DateTime(TimePickerStart.Value.Value.Year, TimePickerStart.Value.Value.Month, TimePickerStart.Value.Value.Day + 1,
                  0, TimePickerStart.Value.Value.Minute, TimePickerStart.Value.Value.Second);
            }
            /// exception situation when we re create appointment on mindate
            if(CellTaped <= DateTime.Now)
            {
                TimePickerStart.Value = DateTime.Now.AddMinutes(10);
                TimePickerStart.MinTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                TimePickerEnd.Value = CellTaped.AddHours(1);
                
            }
        }
        /// <summary>
        /// we have custom context menu, this function useless
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Schedule_AppointmentEditorOpening(object sender, AppointmentEditorOpeningEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// day
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Day_Click(object sender, RoutedEventArgs e)
        {
            Schedule.ViewType = Syncfusion.UI.Xaml.Scheduler.SchedulerViewType.TimelineDay;
        }
        /// <summary>
        /// week
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Week_Click(object sender, RoutedEventArgs e)
        {
            Schedule.ViewType = Syncfusion.UI.Xaml.Scheduler.SchedulerViewType.Week;
        }
        /// <summary>
        /// month
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Month_Click(object sender, RoutedEventArgs e)
        {
            Schedule.ViewType = Syncfusion.UI.Xaml.Scheduler.SchedulerViewType.Month;
        }
        /// <summary>
        /// cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DialogEditorCancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogEditor.IsOpen = false;
        }

        /// <summary>
        /// this variable contain appointment list
        /// </summary>
        public static ScheduleAppointment appointment;
        private void DialogEditSaveButton_Click(object sender, RoutedEventArgs e)
        {
            Schedule.MinimumDate = App.DateTimeNow;
            try
            {
                TimeCollapseBegin = $"{DateTime.Now.Year}-{DatePickerStart.Value.Value.Month}-{DatePickerStart.Value.Value.Day}" +
                    $" {TimePickerStart.Value.Value.Hour}:{TimePickerStart.Value.Value.Minute}:0";

                TimeCollapseEnd = $"{DateTime.Now.Year}-{DatePickerEnd.Value.Value.Month}-{DatePickerEnd.Value.Value.Day} " +
                    $"{TimePickerEnd.Value.Value.Hour}:{TimePickerEnd.Value.Value.Minute}:0";

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

                /// exception situation
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
                    Note = appointment.Notes,
                    UserId = 1,
                    StartTime = appointment.StartTime,
                    EndTime = appointment.EndTime,
                    Title = appointment.Subject

                };

                ///send to web api
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
                TimeCollapseEnd = $"{DateTime.Now.Year}-{DatePickerEnd.Value.Value.Month}-{DatePickerEnd.Value.Value.Day} " +
                    $"{TimePickerEnd.Value.Value.Hour}:{TimePickerEnd.Value.Value.Minute}:0";
               
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
                    Note = appointment.Notes,
                    UserId = 1,
                    StartTime = appointment.StartTime,
                    EndTime = appointment.EndTime,
                    Title = appointment.Subject

                };

                ///send to web api
                var response = App.httpClient.PostAsJsonAsync("appointment", appointment1).Result;


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Бронь была успешно добавлена!");
                }
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("Ошибка!");
                }
            }
        }
        /// <summary>
        /// edit appointment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DialogEditEditButton_Click(object sender, RoutedEventArgs e)
        {

            Appointment appointment = new Appointment() {

                AppointmentCode = AppointmentTapped.AppointmentCode,
                Background = DialogEditorColorpicker.Color.ToString(),
                Note = Description.Text,
                UserId = 1,
                EndTime = DateTime.Parse(TimeCollapseEnd),
                StartTime = DateTime.Parse(TimeCollapseBegin),
                Title = Username.Text,
            };

            var update = App.httpClient.PutAsJsonAsync("appointment", appointment).Result;

            if (update.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("Бронь была изменена!");
            }

            if (update.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                MessageBox.Show("Произошла ошибка!");
            }

            DialogEditor.IsOpen = false;

            var changeConditionAppoinment = list.FirstOrDefault(q=>(int)q.Id == AppointmentTapped.AppointmentCode);
            changeConditionAppoinment.StartTime = DateTime.Parse(TimeCollapseBegin);
            changeConditionAppoinment.EndTime = DateTime.Parse(TimeCollapseEnd);
            changeConditionAppoinment.AppointmentBackground = new SolidColorBrush(DialogEditorColorpicker.Color);
            changeConditionAppoinment.Notes = Description.Text;
            changeConditionAppoinment.Subject = Username.Text;
        }
        /// <summary>
        /// edit context button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            DialogEditEditButton.Visibility = Visibility.Visible;
            DatePickerStart.IsEnabled = false;
            DatePickerEnd.IsEnabled = false;
            DialogEditor.IsOpen = true;
            DialogEditSaveButton.Visibility = Visibility.Collapsed;

            Username.Text = "";
            Description.Text = "";

            if (AppointmentTapped != null)
            {
                DialogEditor.IsOpen = true;
                Username.Text = AppointmentTapped.Title;
                Description.Text = AppointmentTapped.Note;
                DatePickerStart.Value = AppointmentTapped.StartTime;
                TimePickerStart.Value = AppointmentTapped.StartTime;
                DatePickerEnd.Value = AppointmentTapped.EndTime;
                TimePickerEnd.Value = AppointmentTapped.EndTime;
            }
        }
        /// <summary>
        /// delete appointment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentTapped != null)
            {
                foreach (var item in list.ToList().Where(q => (int)q.Id == AppointmentTapped.AppointmentCode))
                {
                    if (item != null)
                    {
                        list.Remove(item);

                        var deleteAppointment = App.httpClient.DeleteAsync($"https://localhost:5001/api/appointment/{item.Id}").Result;


                        if (deleteAppointment.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            MessageBox.Show("Бронь была удалена!");
                        }

                        if (deleteAppointment.StatusCode == System.Net.HttpStatusCode.BadRequest)
                        {
                            MessageBox.Show("Произошла ошибка!");
                        }
                    }
                }
            }

        }
        /// <summary>
        /// datepickerstart
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
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

                TimeCollapseBegin = $"{DateTime.Now.Year}-{DatePickerStart.Value.Value.Month}-{DatePickerStart.Value.Value.Day} {DatePickerStart.Value.Value.Hour}:{DatePickerStart.Value.Value.Minute}:{0}";

                DatePickerStart.Value = DateTime.Parse(TimeCollapseBegin);
                DatePickerEnd.MinDate = DateTime.Parse(DatePickerStart.Value.ToString());
            }
        }

        /// <summary>
        /// datepickerend
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private void DatePickerEnd_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimeCollapseEnd = $"{DateTime.Now.Year}-{DatePickerEnd.Value.Value.Month}-{DatePickerEnd.Value.Value.Day} " +
                $"{DatePickerEnd.Value.Value.Hour}:{DatePickerEnd.Value.Value.Minute}:{0}";
        }

        /// <summary>
        /// timepickerstart
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private void TimePickerStart_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(TimePickerStart != null)
                TimeCollapseBegin = TimePickerStart.Value.ToString();
          
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

            if (CellTaped <= DateTime.Now)
            {
                TimePickerStart.Value = DateTime.Now;
            }
        }
        /// <summary>
        /// timepicerkend
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private void TimePickerEnd_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (TimePickerEnd != null)
            {
                TimeCollapseEnd = TimePickerEnd.Value.ToString();
            }
        }
        /// <summary>
        /// colorpicker temp
        /// </summary>
        public string ColorPicker { get; set; }
        /// <summary>
        /// colorpicker
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private void DialogEditorColorpicker_ColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorPicker = DialogEditorColorpicker.Color.ToString();
        }
    }
}
