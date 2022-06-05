using System;
using System.Windows.Media;

namespace WpfApp2.Model
{
    public class Appointment
    {
        public int AppointmentCode { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Background { get; set; }

        public int UserId { get; set; }
    }
}
