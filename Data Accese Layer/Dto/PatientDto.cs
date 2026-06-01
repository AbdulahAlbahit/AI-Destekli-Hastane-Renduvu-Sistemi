using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Accese_Layer.Dto
{
    public class PatientDto
    {
        public int PatientId { get; set; }

        public string PatientName { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }

        public string Gender { get; set; } = null!;

        public string? Phone { get; set; }

    }
}
