using Syncfusion.UI.Xaml.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace WpfApp2.ViewModel
{
    public class BookingViewModel
    {
        public static ObservableCollection<ScheduleAppointment> Collection { get; set; } 
            = new ObservableCollection<ScheduleAppointment>();

        public string Username { get; set; } = "Musa";
        public BookingViewModel()
        {

        }
    }
}
