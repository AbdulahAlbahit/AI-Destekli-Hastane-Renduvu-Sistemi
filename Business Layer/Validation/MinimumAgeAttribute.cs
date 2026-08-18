using System;
using System.ComponentModel.DataAnnotations;

namespace Business_Layer.Validation
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateOnly dateOfBirth)
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                var age = today.Year - dateOfBirth.Year;
                if (dateOfBirth > today.AddYears(-age)) age--;

                if (age < _minimumAge)
                {
                    return new ValidationResult(ErrorMessage ?? $"Kayıt olabilmek için en az {_minimumAge} yaşında olmalısınız.");
                }
            }
            else if (value is string dobString && DateOnly.TryParse(dobString, out DateOnly parsedDob))
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                var age = today.Year - parsedDob.Year;
                if (parsedDob > today.AddYears(-age)) age--;

                if (age < _minimumAge)
                {
                    return new ValidationResult(ErrorMessage ?? $"Kayıt olabilmek için en az {_minimumAge} yaşında olmalısınız.");
                }
            }
            
            return ValidationResult.Success;
        }
    }
}
