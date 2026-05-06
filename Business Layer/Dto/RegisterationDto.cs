using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Dto
{
    public class RegisterationDto
    {

        public string TC { get; set; }
        public string Password { get; set; }
        public string PatientName { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }

        public string Gender { get; set; } = null!;

        public string? Phone { get; set; }

    }
}
