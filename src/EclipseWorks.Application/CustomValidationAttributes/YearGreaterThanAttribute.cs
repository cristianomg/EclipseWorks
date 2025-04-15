using System.ComponentModel.DataAnnotations;

namespace EclipseWorks.Application.CustomValidateAttributes
{
    public class YearGreaterThanAttribute : ValidationAttribute
    {
        private readonly int _year;
        public YearGreaterThanAttribute(int year)
        {
            _year = year;
            ErrorMessage = $"The year must be great than {_year}";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue.Year > _year)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return new ValidationResult("Invalid date.");
        }
    }
}
