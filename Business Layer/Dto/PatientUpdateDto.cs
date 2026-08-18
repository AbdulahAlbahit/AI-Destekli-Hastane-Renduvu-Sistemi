using System;
using Business_Layer.Validation;

namespace Business_Layer.Dto
{
    public class PatientUpdateDto
    {
        public string PatientName { get; set; } = null!;
        public string? Phone { get; set; }
        [MinimumAge(18, ErrorMessage = "Profilinizi güncelleyebilmek için en az 18 yaşında olmalısınız.")]
        public string DateOfBirth { get; set; } // yyyy-MM-dd formatından DateOnly'ye parse edeceğiz
        public string Gender { get; set; } = null!;
    }
}
