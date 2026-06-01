using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Accese_Layer.Dto
{
    public class TheAppointmentTimes
    {
        public int AppointmentId { get; set; }

        public DateOnly TheDate { get; set; }

        public TimeOnly TheTime { get; set; }
    }
}
