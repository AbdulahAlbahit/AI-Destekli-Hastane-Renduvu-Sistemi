using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Dto
{
    
        public class AppointmentCreateDto
        {
            public DateOnly TheDate { get; set; }
            public TimeOnly TheTime { get; set; }
            public string TheStatus { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int ClinicId { get; set; }
    }
    }

