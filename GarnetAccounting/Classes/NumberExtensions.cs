public static class NumberExtensions
{
    private static readonly string[] Units = { "", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
    private static readonly string[] Teens = { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
    private static readonly string[] Tens = { "", "", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
    private static readonly string[] Hundreds = { "", "یکصد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" };
    private static readonly string[] ThousandsGroups = { "", "هزار", "میلیون", "میلیارد", "تریلیون" };

    public static string ToPersianWords(this long number)
    {
        if (number == 0)
            return "صفر ریال";

        if (number < 0)
            return "منفی " + ToPersianWords(-number);

        string words = "";
        int groupIndex = 0;

        while (number > 0)
        {
            int threeDigits = (int)(number % 1000);
            if (threeDigits != 0)
            {
                string groupText = ConvertThreeDigits(threeDigits);
                if (!string.IsNullOrEmpty(ThousandsGroups[groupIndex]))
                    groupText += " " + ThousandsGroups[groupIndex];

                if (!string.IsNullOrEmpty(words))
                    words = groupText + " و " + words;
                else
                    words = groupText;
            }

            number /= 1000;
            groupIndex++;
        }

        return words + " ریال";
    }
    public static string ToPersianWords(this decimal number)
    {
        if (number == 0)
            return "صفر ریال";

        if (number < 0)
            return "منفی " + ToPersianWords(-number);

        string words = "";
        int groupIndex = 0;

        while (number > 0)
        {
            int threeDigits = (int)(number % 1000);
            if (threeDigits != 0)
            {
                string groupText = ConvertThreeDigits(threeDigits);
                if (!string.IsNullOrEmpty(ThousandsGroups[groupIndex]))
                    groupText += " " + ThousandsGroups[groupIndex];

                if (!string.IsNullOrEmpty(words))
                    words = groupText + " و " + words;
                else
                    words = groupText;
            }

            number /= 1000;
            groupIndex++;
        }

        return words + " ریال";
    }
    private static string ConvertThreeDigits(int number)
    {
        string result = "";

        int hundreds = number / 100;
        int remainder = number % 100;

        if (hundreds != 0)
            result += Hundreds[hundreds];

        if (remainder != 0)
        {
            if (!string.IsNullOrEmpty(result))
                result += " و ";

            if (remainder < 10)
                result += Units[remainder];
            else if (remainder < 20)
                result += Teens[remainder - 10];
            else
            {
                int tens = remainder / 10;
                int units = remainder % 10;
                result += Tens[tens];
                if (units != 0)
                    result += " و " + Units[units];
            }
        }

        return result;
    }
}
