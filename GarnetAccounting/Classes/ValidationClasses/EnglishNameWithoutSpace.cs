using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GarnetAccounting.Classes.ValidationClasses
{
    public class EnglishNameWithoutSpace : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult("فیلد نباید خالی باشد.");
            }

            if (!Regex.IsMatch(value.ToString(), @"^[A-Za-z]+$"))
            {
                return new ValidationResult("فقط حروف الفبای انگلیسی و بدون فاصله مجاز است.");
            }

            return ValidationResult.Success;
        }
    }
}
