using System.Text.RegularExpressions;

namespace GarnetAccounting.Classes.ValidationClasses
{
    public static class ValidatorUtil
    {

        //اعتبار سنجی کد ملی 10 رقمی
        //public static bool IsValidPersonNationalCode(string input)
        //{
        //    if (!long.TryParse(input, out long num) || input.Length != 10)
        //        return false;

        //    int check = int.Parse(input.Substring(9, 1));
        //    int sum = Enumerable.Range(0, 9)
        //                        .Select(x => int.Parse(input.Substring(x, 1)) * (10 - x))
        //                        .Sum() % 11;

        //    return (sum < 2 && check == sum) || (sum >= 2 && check + sum == 11);
        //}
        public static bool IsValidNationalCode(string nationalCode)
        {
            // بررسی طول کد ملی
            if (string.IsNullOrEmpty(nationalCode) || nationalCode.Length != 10)
            {
                return false;
            }

            // بررسی کد ملی‌های یکسان که نامعتبر هستند
            if (nationalCode.All(c => c == nationalCode[0]))
            {
                return false;
            }

            // محاسبه رقم کنترل
            int controlDigit = int.Parse(nationalCode.Substring(9, 1));
            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(nationalCode[i].ToString()) * (10 - i);
            }

            int remainder = sum % 11;

            // بررسی رقم کنترل
            return remainder < 2 ? controlDigit == remainder : controlDigit == 11 - remainder;
        }

        //بررسی صحت شناسه ملی اشخاص حقوقی
        public static bool IsValidCompanyNationalId(string nationalId)
        {
            // بررسی طول شناسه ملی
            if (nationalId.Length != 11)
            {
                return false;
            }

            // جدا کردن رقم کنترل (آخرین رقم)
            int controlDigit = int.Parse(nationalId.Substring(10, 1));

            // محاسبه مجموع با توجه به الگوریتم ارائه شده
            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                int digit = int.Parse(nationalId.Substring(i, 1));
                int coefficient = (i % 6) + 17; // تعیین ضریب بر اساس موقعیت رقم
                sum += (digit + 11) * coefficient; // اضافه کردن 11 به هر رقم و ضرب در ضریب
            }

            // محاسبه باقیمانده
            int remainder = sum % 11;

            // اگر باقیمانده برابر 10 باشد، آن را به 0 تغییر دهیم
            if (remainder == 10)
            {
                remainder = 0;
            }

            // بررسی صحت رقم کنترل
            return remainder == controlDigit;
        }


        //// اعتبار سنجی شناسه ملی 11 رقمی
        //public static bool IsValidIranianNationalId(string id)
        //{
        //    if (!long.TryParse(id, out long num) || id.Length != 11)
        //        return false;

        //    int check = Convert.ToInt32(id.Substring(10, 1));
        //    int sum = Enumerable.Range(1, 10)
        //                        .Select(x => Convert.ToInt32(id.Substring(x - 1, 1)) * (11 - x))
        //                        .Sum() % 11;

        //    return (sum < 2 && check == sum) || (sum >= 2 && check + sum == 11);
        //}

        // جمله انگلیسی بدون فاصله
        public static bool IsEnglishNameWithoutSpace(string input)
        {
            return !string.IsNullOrEmpty(input) && Regex.IsMatch(input, @"^[A-Za-z]+$");
        }

        public static bool IsFileSizeValid(string filePath, int maxSizeInKB = 500)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            long sizeInBytes = fileInfo.Length;
            long sizeInKB = sizeInBytes / 1024;
            return sizeInKB <= maxSizeInKB;
        }


    }
}
