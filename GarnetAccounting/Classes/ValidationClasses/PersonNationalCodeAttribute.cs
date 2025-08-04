using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Classes.ValidationClasses
{
    public class PersonNationalCodeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var input = value as string;

            if (string.IsNullOrWhiteSpace(input))
                return new ValidationResult("کد ملی نباید خالی باشد.");

            if (!long.TryParse(input, out long num) || input.Length != 10)
                return new ValidationResult("کد ملی باید ۱۰ رقم باشد.");

            int check = int.Parse(input.Substring(9, 1));
            int sum = Enumerable.Range(0, 9)
                                .Select(x => int.Parse(input.Substring(x, 1)) * (10 - x))
                                .Sum() % 11;

            if ((sum < 2 && check == sum) || (sum >= 2 && check + sum == 11))
                return ValidationResult.Success;

            return new ValidationResult("کد ملی نامعتبر است.");
        }
    }

}
