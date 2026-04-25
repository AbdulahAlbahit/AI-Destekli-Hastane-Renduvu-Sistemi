using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Accese_Layer.Dto
{
    public class DoctorDetailDto
    {
        public int DoctorId { get; set; }

        public string DoctorName { get; set; } = null!;

        public string Specialization { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string? Email { get; set; }
        public int ClinicNumber { get; set; }
        public string DepName { get; set; }

    }
}
