using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Classes.ValidationClasses
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class FileSizeAttribute : ValidationAttribute
    {
        public int MaxSizeInKB { get; }

        public FileSizeAttribute(int maxSizeInKB)
        {
            MaxSizeInKB = maxSizeInKB;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            if (value is string path)
            {
                if (File.Exists(path))
                {
                    var fileInfo = new FileInfo(path);
                    long sizeInBytes = fileInfo.Length;
                    long sizeInKB = sizeInBytes / 1024;

                    if (sizeInKB > MaxSizeInKB)
                    {
                        return new ValidationResult(GetErrorMessage());
                    }
                }
                else
                {
                    return new ValidationResult("فایل مورد نظر یافت نشد.");
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"حجم فایل نباید بیشتر از {MaxSizeInKB} کیلوبایت باشد.";
        }
    }
}
