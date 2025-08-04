using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Classes.ValidationClasses
{
    public class NationlIdentifireIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var id = value as string;

            if (string.IsNullOrWhiteSpace(id))
                return new ValidationResult("شناسه ملی نباید خالی باشد.");

            if (!long.TryParse(id, out long num) || id.Length != 11)
                return new ValidationResult("شناسه ملی باید دقیقاً ۱۱ رقم باشد.");

            int check = Convert.ToInt32(id.Substring(10, 1));
            int sum = Enumerable.Range(1, 10)
                                .Select(x => Convert.ToInt32(id.Substring(x - 1, 1)) * (11 - x))
                                .Sum() % 11;

            if ((sum < 2 && check == sum) || (sum >= 2 && check + sum == 11))
                return ValidationResult.Success;

            return new ValidationResult("شناسه ملی نامعتبر است.");
        }
    }


}
