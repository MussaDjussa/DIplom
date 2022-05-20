using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApp2.ViewModel
{
    public class ScheduleViewModel
    {
        public ObservableCollection<Meeting> Meetings { get; set; }

        public ScheduleViewModel()
        {
            Meetings = new ObservableCollection<Meeting>() {

               new Meeting(){ Color = Brushes.Blue, From = DateTime.Today.AddHours(13), To = DateTime.Today.AddHours(15), IsAllDay = false }
           };

        }
    }
}
