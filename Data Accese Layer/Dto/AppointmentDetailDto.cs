using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Accese_Layer.Dto
{
    public class AppointmentDetailDto
    {

        public int AppointmentId { get; set; }

        public DateOnly TheDate { get; set; }

        public TimeOnly TheTime { get; set; }
        public string TheStatus { get; set; } = null!;
        public string DoctorName { get; set; } 
        public string DepName {  get; set; }
        public int ClinicNum {  get; set; }
        public string PatientGender { get; set; } = null!;

        public string? PatientPhone { get; set; }

          public string PatientName { get; set; } = null!;


    }
}
