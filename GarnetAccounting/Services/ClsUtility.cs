using MD.PersianDateTime.Standard;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;



public static class Extension
{

    public static DateTime PersianToLatin(this string PersianDate)
    {
        PersianCalendar pc = new PersianCalendar();

        string year = PersianDate.Substring(0, 4);
        string month = PersianDate.Substring(5, 2);
        string day = PersianDate.Substring(8, 2);

        int yyyy = Convert.ToInt32(year);
        int mm = Convert.ToInt32(month);
        int dd = Convert.ToInt32(day);

        DateTime dt = pc.ToDateTime(yyyy, mm, dd, 0, 0, 0, 0);

        return dt;

    }
    public static DateTime PersianToLatinRefah(this string PersianDate)
    {
        PersianCalendar pc = new PersianCalendar();

        string year = PersianDate.Substring(1, 4);
        string month = PersianDate.Substring(6, 2);
        string day = PersianDate.Substring(9, 2);

        int yyyy = Convert.ToInt32(year);
        int mm = Convert.ToInt32(month);
        int dd = Convert.ToInt32(day);

        DateTime dt = pc.ToDateTime(yyyy, mm, dd, 0, 0, 0, 0);

        return dt;

    }
    public static string mdToLongDateString(this DateTime date)
    {
        PersianDateTime dt = new PersianDateTime(date);
        return dt.ToString();
    }
    public static string mdToLongPersianDate(this DateTime date)
    {
        PersianDateTime dt = new PersianDateTime(date);
        return dt.ToLongDateString();
    }

    public static DateTime mdToMiladiDate(this string date)
    {
        PersianDateTime dt = PersianDateTime.Parse(date);
        return dt.ToDateTime();
    }
    public static string LatinToPersian(this DateTime LatinDate)
    {

        try
        {
            PersianCalendar pc = new PersianCalendar();
            string yyyy = pc.GetYear(LatinDate).ToString("0000");
            string mm = pc.GetMonth(LatinDate).ToString("00");
            string dd = pc.GetDayOfMonth(LatinDate).ToString("00");

            string persianDate = yyyy + "/" + mm + "/" + dd;

            return persianDate;
        }
        catch
        {
            return "-";
        }

    }
    public static string LatinToPersianForDatepicker(this DateTime LatinDate)
    {
        PersianCalendar pc = new PersianCalendar();

        string yyyy = pc.GetYear(LatinDate).ToString("0000");
        string mm = pc.GetMonth(LatinDate).ToString("00");
        string dd = pc.GetDayOfMonth(LatinDate).ToString("00");

        string persianDate = yyyy + "-" + mm + "-" + dd;

        return persianDate;

    }
    public static string LatinToPersian(string date)
    {
        PersianCalendar pc = new PersianCalendar();

        DateTime LatinDate = Convert.ToDateTime(date);

        string yyyy = pc.GetYear(LatinDate).ToString("0000");
        string mm = pc.GetMonth(LatinDate).ToString("00");
        string dd = pc.GetDayOfMonth(LatinDate).ToString("00");

        string persianDate = yyyy + "/" + mm + "/" + dd;

        return persianDate;
    }




    public static string mdToPersianDate(this DateTime date)
    {
        PersianDateTime dt = new PersianDateTime(date);
        return dt.ToString();
    }


    public enum ImageComperssion
    {
        Maximum = 50,
        Good = 60,
        Normal = 70,
        Fast = 80,
        Minimum = 90,
    }
    public static string GetMimeType(this string Extension)
    {
        var types = GetMimeTypes();
        return types[Extension];
    }

    public static string GetContentType(this string path)
    {
        var types = GetMimeTypes();
        var ext = Path.GetExtension(path).ToLowerInvariant();
        return types[ext];
    }

    private static Dictionary<string, string> GetMimeTypes()
    {
        return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".zip","application/zip" },
                {".rar","application/x-rar" }
            };
    }



    public static string ToLowerFirst(this string str)
    {
        return str.Substring(0, 1).ToLower() + str.Substring(1);
    }

    public static DateTime ToPersianDate(this DateTime dt)
    {
        PersianCalendar pc = new PersianCalendar();
        int year = pc.GetYear(dt);
        int month = pc.GetMonth(dt);
        int day = pc.GetDayOfMonth(dt);
        int hour = pc.GetHour(dt);
        int min = pc.GetMinute(dt);

        return new DateTime(year, month, day, hour, min, 0);
    }

    public static DateTime ToMiladiDate(this DateTime dt)
    {
        PersianCalendar pc = new PersianCalendar();
        return pc.ToDateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0, 0);

    }

    public static string ToPrice(this object dec)
    {
        if (dec == null) return "0";

        string Src = dec.ToString();
        Src = Src.Replace(".0000", "");
        if (!Src.Contains("."))
        {
            Src = Src + ".00";
        }
        string[] price = Src.Split('.');
        if (price[1].Length >= 2)
        {
            price[1] = price[1].Substring(0, 2);
            price[1] = price[1].Replace("00", "");
        }

        string Temp = null;
        int i = 0;
        if ((price[0].Length % 3) >= 1)
        {
            Temp = price[0].Substring(0, (price[0].Length % 3));
            for (i = 0; i <= (price[0].Length / 3) - 1; i++)
            {
                Temp += "," + price[0].Substring((price[0].Length % 3) + (i * 3), 3);
            }
        }
        else
        {
            for (i = 0; i <= (price[0].Length / 3) - 1; i++)
            {
                Temp += price[0].Substring((price[0].Length % 3) + (i * 3), 3) + ",";
            }
            Temp = Temp.Substring(0, Temp.Length - 1);
            // Temp = price(0)
            //If price(1).Length > 0 Then
            //    Return price(0) + "." + price(1)
            //End If
        }
        if (price[1].Length > 0)
        {
            return Temp + "." + price[1];
        }
        else
        {
            return Temp;
        }
    }

    public static string Encrypt(this string str)
    {
        byte[] encData_byte = new byte[str.Length];
        encData_byte = System.Text.Encoding.UTF8.GetBytes(str);
        return Convert.ToBase64String(encData_byte);
    }

    public static string Decrypt(this string str)
    {
        System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
        System.Text.Decoder utf8Decode = encoder.GetDecoder();
        byte[] todecode_byte = Convert.FromBase64String(str);
        int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        char[] decoded_char = new char[charCount];
        utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        return new string(decoded_char);
    }


    public static bool IsUrl(this string str)
    {
        return Regex.IsMatch(str, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        //return Regex.IsMatch(str, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?");
    }

    public static bool IsMobile(this string str)
    {
        return Regex.IsMatch(str, @"^(((\+|00)98)|0)?9[123]\d{8}$");
    }

    public static bool IsTimeSpan12(this string str)
    {
        return Regex.IsMatch(str, @"^(1[012]|[1-9]):([0-5]?[0-9]) (AM|am|PM|pm)$");
    }

    public static bool IsTimeSpan12P(this string str)
    {
        return Regex.IsMatch(str, @"^(1[012]|[1-9]):([0-5]?[0-9]) (ق ظ|ق.ظ|ب ظ|ب.ظ)$");
    }

    public static bool IsTimeSpan24hhm(this string str)
    {
        return Regex.IsMatch(str, @"^([01][0-9]|2[0-3]):([0-5]?[0-9])$");
    }

    public static bool IsTimeSpan24hm(this string str)
    {
        return Regex.IsMatch(str, @"^(2[0-3]|[01]?\d):([0-5]?[0-9])$");
    }

    public static bool IsPersianDateTime(this string str)
    {
        return Regex.IsMatch(str, @"^(13\d{2}|[1-9]\d)/(1[012]|0?[1-9])/([12]\d|3[01]|0?[1-9]) ([01][0-9]|2[0-3]):([0-5]?[0-9])$");
    }

    public static bool IsTime(this string str)
    {
        return Regex.IsMatch(str, @"^([01][0-9]|2[0-3]):([0-5]?[0-9])$");
    }

    public static bool IsPersianDate(this string str)
    {
        return Regex.IsMatch(str, @"^(13\d{2}|[1-9]\d)/(1[012]|0?[1-9])/([12]\d|3[01]|0?[1-9])$");
    }

    public static string ToStringShamsiDate(this DateTime dt)
    {
        System.Globalization.PersianCalendar PC = new System.Globalization.PersianCalendar();
        int intYear = PC.GetYear(dt);
        int intMonth = PC.GetMonth(dt);
        int intDayOfMonth = PC.GetDayOfMonth(dt);
        DayOfWeek enDayOfWeek = PC.GetDayOfWeek(dt);
        string strMonthName = "";
        string strDayName = "";
        switch (intMonth)
        {
            case 1:
                strMonthName = "فروردین";
                break;
            case 2:
                strMonthName = "اردیبهشت";
                break;
            case 3:
                strMonthName = "خرداد";
                break;
            case 4:
                strMonthName = "تیر";
                break;
            case 5:
                strMonthName = "مرداد";
                break;
            case 6:
                strMonthName = "شهریور";
                break;
            case 7:
                strMonthName = "مهر";
                break;
            case 8:
                strMonthName = "آبان";
                break;
            case 9:
                strMonthName = "آذر";
                break;
            case 10:
                strMonthName = "دی";
                break;
            case 11:
                strMonthName = "بهمن";
                break;
            case 12:
                strMonthName = "اسفند";
                break;
            default:
                strMonthName = "";
                break;
        }

        //switch (enDayOfWeek)
        //{
        //    case DayOfWeek.Friday:
        //        strDayName = "جمعه";
        //        break;
        //    case DayOfWeek.Monday:
        //        strDayName = "دوشنبه";
        //        break;
        //    case DayOfWeek.Saturday:
        //        strDayName = "شنبه";
        //        break;
        //    case DayOfWeek.Sunday:
        //        strDayName = "یکشنبه";
        //        break;
        //    case DayOfWeek.Thursday:
        //        strDayName = "پنجشنبه";
        //        break;
        //    case DayOfWeek.Tuesday:
        //        strDayName = "سه شنبه";
        //        break;
        //    case DayOfWeek.Wednesday:
        //        strDayName = "چهارشنبه";
        //        break;
        //    default:
        //        strDayName = "";
        //        break;
        //}

        return (string.Format("{0} {1} {2} {3}", strDayName, intDayOfMonth, strMonthName, intYear));
    }

    public static string ToText(this int digit)
    {
        string txt = digit.ToString();
        int length = txt.Length;

        string[] a1 = new string[10] { "-", "یک", "دو", "سه", "چهار", "پنح", "شش", "هفت", "هشت", "نه" };
        string[] a2 = new string[10] { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
        string[] a3 = new string[10] { "-", "ده", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
        string[] a4 = new string[10] { "-", "یک صد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفصد", "هشصد", "نهصد" };

        string result = "";
        bool isDahegan = false;

        for (int i = 0; i < length; i++)
        {
            string character = txt[i].ToString();
            switch (length - i)
            {
                case 7://میلیون
                    if (character != "0")
                    {
                        result += a1[Convert.ToInt32(character)] + " میلیون و ";
                    }
                    else
                    {
                        result = result.TrimEnd('و', ' ');
                    }
                    break;
                case 6://صدهزار
                    if (character != "0")
                    {
                        result += a4[Convert.ToInt32(character)] + " و ";
                    }
                    else
                    {
                        result = result.TrimEnd('و', ' ');
                    }
                    break;
                case 5://ده هزار
                    if (character == "1")
                    {
                        isDahegan = true;
                    }
                    else if (character != "0")
                    {
                        result += a3[Convert.ToInt32(character)] + " و ";
                    }
                    break;
                case 4://هزار
                    if (isDahegan == true)
                    {
                        result += a2[Convert.ToInt32(character)] + " هزار و ";
                        isDahegan = false;
                    }
                    else
                    {
                        if (character != "0")
                        {
                            result += a1[Convert.ToInt32(character)] + " هزار و ";
                        }
                        else
                        {
                            result = result.TrimEnd('و', ' ');
                        }
                    }
                    break;
                case 3://صد
                    if (character != "0")
                    {
                        result += a4[Convert.ToInt32(character)] + " و ";
                    }
                    break;
                case 2://ده
                    if (character == "1")
                    {
                        isDahegan = true;
                    }
                    else if (character != "0")
                    {
                        result += a3[Convert.ToInt32(character)] + " و ";
                    }
                    break;
                case 1://یک
                    if (isDahegan == true)
                    {
                        result += a2[Convert.ToInt32(character)];
                        isDahegan = false;
                    }
                    else
                    {
                        if (character != "0") result += a1[Convert.ToInt32(character)];
                        else result = result.TrimEnd('و', ' ');
                    }
                    break;
            }
        }
        return result;
    }

    public static string ToText(this long digit)
    {
        string txt = digit.ToString();
        int length = txt.Length;

        string[] a1 = new string[10] { "-", "یک", "دو", "سه", "چهار", "پنح", "شش", "هفت", "هشت", "نه" };
        string[] a2 = new string[10] { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
        string[] a3 = new string[10] { "-", "ده", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
        string[] a4 = new string[10] { "-", "یک صد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفصد", "هشصد", "نهصد" };

        string result = "";
        bool isDahegan = false;

        for (int i = 0; i < length; i++)
        {
            string character = txt[i].ToString();
            switch (length - i)
            {
                case 7://میلیون
                    if (character != "0")
                    {
                        result += a1[Convert.ToInt32(character)] + " میلیون و ";
                    }
                    else
                    {
                        result = result.TrimEnd('و', ' ');
                    }
                    break;
                case 6://صدهزار
                    if (character != "0")
                    {
                        result += a4[Convert.ToInt32(character)] + " و ";
                    }
                    else
                    {
                        result = result.TrimEnd('و', ' ');
                    }
                    break;
                case 5://ده هزار
                    if (character == "1")
                    {
                        isDahegan = true;
                    }
                    else if (character != "0")
                    {
                        result += a3[Convert.ToInt32(character)] + " و ";
                    }
                    break;
                case 4://هزار
                    if (isDahegan == true)
                    {
                        result += a2[Convert.ToInt32(character)] + " هزار و ";
                        isDahegan = false;
                    }
                    else
                    {
                        if (character != "0")
                        {
                            result += a1[Convert.ToInt32(character)] + " هزار و ";
                        }
                        else
                        {
                            result = result.TrimEnd('و', ' ');
                        }
                    }
                    break;
                case 3://صد
                    if (character != "0")
                    {
                        result += a4[Convert.ToInt32(character)] + " و ";
                    }
                    break;
                case 2://ده
                    if (character == "1")
                    {
                        isDahegan = true;
                    }
                    else if (character != "0")
                    {
                        result += a3[Convert.ToInt32(character)] + " و ";
                    }
                    break;
                case 1://یک
                    if (isDahegan == true)
                    {
                        result += a2[Convert.ToInt32(character)];
                        isDahegan = false;
                    }
                    else
                    {
                        if (character != "0") result += a1[Convert.ToInt32(character)];
                        else result = result.TrimEnd('و', ' ');
                    }
                    break;
            }
        }
        return result;
    }

    public static bool CheckAmount(this string digit)
    {

        try
        {
            Decimal tmp = Convert.ToDecimal(digit);
            if (tmp > 0)
                return true;
            else
                return false;
        }
        catch
        {
            return false;
        }
    }
    public static DataTable ToDataTable<T>(this List<T> iList)
    {
        DataTable dataTable = new DataTable();
        PropertyDescriptorCollection propertyDescriptorCollection =
            TypeDescriptor.GetProperties(typeof(T));
        for (int i = 0; i < propertyDescriptorCollection.Count; i++)
        {
            PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
            Type type = propertyDescriptor.PropertyType;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                type = Nullable.GetUnderlyingType(type);


            dataTable.Columns.Add(propertyDescriptor.Name, type);
        }
        object[] values = new object[propertyDescriptorCollection.Count];
        foreach (T iListItem in iList)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
            }
            dataTable.Rows.Add(values);
        }
        return dataTable;
    }

    public static string ToInvoicTypeName(this Int16 InvoiceType)
    {
        string str = "";

        switch (InvoiceType)
        {
            case 1:
                str = "خرید";
                break;
            case 2:
                str = "فروش";
                break;
            case 3:
                str = "صورت وضعیت";
                break;
            case 4:
                str = "پیش فاکتور";
                break;
            default:
                break;
        }

        return str;
    }

    public static string ToPaymentName(this Int16 InvoiceType)
    {
        string str = "";

        switch (InvoiceType)
        {
            case 0:
                str = "نقدی";
                break;
            case 1:
                str = "بانک";
                break;

            default:
                break;
        }

        return str;
    }

    public static bool IsValidNationalCode(this string txt)
    {
        //در صورتی که کد ملی وارد شده تهی باشد
        if (string.IsNullOrEmpty(txt))
            throw new Exception("کد ملی را بدرستی وارد نمایید");


        if (txt.Length < 10)
            throw new Exception("");

        //در صورتی که کد ملی وارد شده طولش کمتر از 10 رقم باشد
        if (txt.Length != 10)
            throw new Exception("طول کد ملی باید ده کاراکتر باشد");

        //در صورتی که کد ملی ده رقم عددی نباشد
        var regex = new Regex(@"\d{10}");
        if (!regex.IsMatch(txt))
            throw new Exception("کد ملی تشکیل شده از ده رقم عددی می‌باشد؛ لطفا کد ملی را صحیح وارد نمایید");

        //در صورتی که رقم‌های کد ملی وارد شده یکسان باشد
        var allDigitEqual = new[] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
        if (allDigitEqual.Contains(txt)) return false;

        var chArray = txt.ToCharArray();
        var num0 = Convert.ToInt32(chArray[0].ToString()) * 10;
        var num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
        var num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
        var num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
        var num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
        var num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
        var num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
        var num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
        var num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
        var a = Convert.ToInt32(chArray[9].ToString());

        var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
        var c = b % 11;

        return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)));
    }
    public static bool IsValidNationalId(this string txt)
    {
        //در صورتی که کد ملی وارد شده تهی باشد
        if (string.IsNullOrEmpty(txt))
            throw new Exception("کد ملی را بدرستی وارد نمایید");


        if (txt.Length < 11)
            throw new Exception("شناسه ملی باید 11 رقم باشد");

        //در صورتی که شناسه ملی وارد شده طولش کمتر از 11 رقم باشد
        if (txt.Length != 11)
            throw new Exception("طول شناسه ملی باید یازده کاراکتر باشد");

        //در صورتی که شناسه ملی ده رقم عددی نباشد
        var regex = new Regex(@"\d{10}");
        if (!regex.IsMatch(txt))
            throw new Exception("شناسه ملی تشکیل شده از یازده رقم عددی می‌باشد؛ لطفا شناسه ملی را صحیح وارد نمایید");


        var chArray = txt.ToCharArray();

        int b = Convert.ToInt32(chArray[9].ToString()) + 2;

        var num1 = (Convert.ToInt32(chArray[0].ToString()) + b) * 29;
        var num2 = (Convert.ToInt32(chArray[1].ToString()) + b) * 27;
        var num3 = (Convert.ToInt32(chArray[2].ToString()) + b) * 23;
        var num4 = (Convert.ToInt32(chArray[3].ToString()) + b) * 19;
        var num5 = (Convert.ToInt32(chArray[4].ToString()) + b) * 17;
        var num6 = (Convert.ToInt32(chArray[5].ToString()) + b) * 29;
        var num7 = (Convert.ToInt32(chArray[6].ToString()) + b) * 27;
        var num8 = (Convert.ToInt32(chArray[7].ToString()) + b) * 23;
        var num9 = (Convert.ToInt32(chArray[8].ToString()) + b) * 19;
        var num10 = (Convert.ToInt32(chArray[9].ToString()) + b) * 17;

        int numCheck = Convert.ToInt32(chArray[10].ToString());

        int c = num1 + num10 + num2 + num3 + num4 + num5 + num6 + num7 + num8 + num9;

        int d = c % b;

        return (d == numCheck);
    }
    //بررسی صحت شناسه ملی اشخاص حقوقی
    public static bool IsValidCompanyNationalId(this string nationalId)
    {
        if (string.IsNullOrEmpty(nationalId)) return false;
        // بررسی طول شناسه ملی
        if (nationalId.Length != 11)
        {
            return false;
        }
        return true;
    }
    public static string ToPatternName(this int PatternCode)
    {
        string str = "";

        switch (PatternCode)
        {
            case 1:
                str = "فروش";
                break;
            case 2:
                str = "ارزی";
                break;
            case 3:
                str = "طلا، جواهر و پلاتین";
                break;
            case 4:
                str = "قرارداد پیمانکاری";
                break;
            case 5:
                str = "صادرات";
                break;

            default:
                break;
        }

        return str;
    }

    public static string ToInvoiceSubjectName(this int InvoiceSubjectCode)
    {
        string str = "";

        switch (InvoiceSubjectCode)
        {
            case 1:
                str = "اصلی";
                break;
            case 2:
                str = "اصلاحی";
                break;
            case 3:
                str = "ابطالی";
                break;
            case 4:
                str = "برگشت از فروش";
                break;
            case 5:
                str = "خرید";
                break;
            case 6:
                str = "برگشت از خرید";
                break;

            default:
                break;
        }

        return str;
    }


    public static string ToInvoiceStatusName(this Int16 InvoiceStatusCode)
    {
        string str = "";

        switch (InvoiceStatusCode)
        {
            case 1:
                str = "ارسال نشده";
                break;
            case 2:
                str = "در انتظار تأیید";
                break;
            case 3:
                str = "تأیید شده";
                break;
            case 4:
                str = "اصلاح شده";
                break;
            case 5:
                str = "برگشت از فروش شده";
                break;
            case 6:
                str = "باطل شده";
                break;
            case 7:
                str = "نیاز به اصلاح";
                break;
            default:
                break;
        }

        return str;
    }
    public static string ToSettelmentName(this int SettelmentTypeId)
    {
        string str = "";

        switch (SettelmentTypeId)
        {
            case 1:
                str = "نقدی";
                break;
            case 2:
                str = "نسیه";
                break;
            case 3:
                str = "نقد و نسیه";
                break;
            default:
                break;
        }

        return str;
    }
    public static string ToStatusClassName(this Int16 InvoiceStatusCode)
    {
        string str = "";

        switch (InvoiceStatusCode)
        {
            case 1:
                str = "bg-label-primary";
                break;
            case 2:
                str = "bg-label-secondary";
                break;
            case 3:
                str = "bg-label-success";
                break;
            case 4:
                str = "bg-label-info";
                break;
            case 5:
                str = "bg-label-warning";
                break;
            case 6:
                str = "bg-label-danger";
                break;
            default:
                break;
        }

        return str;
    }

    //=======================================

    private static readonly Dictionary<char, string> persianToLatinMap = new Dictionary<char, string>
    {
        {'ا', "a"}, {'ب', "b"}, {'پ', "p"}, {'ت', "t"}, {'ث', "s"}, {'ج', "j"},
        {'چ', "ch"}, {'ح', "h"}, {'خ', "kh"}, {'د', "d"}, {'ذ', "z"}, {'ر', "r"},
        {'ز', "z"}, {'ژ', "zh"}, {'س', "s"}, {'ش', "sh"}, {'ص', "s"}, {'ض', "z"},
        {'ط', "t"}, {'ظ', "z"}, {'ع', "a"}, {'غ', "gh"}, {'ف', "f"}, {'ق', "gh"},
        {'ک', "k"}, {'گ', "g"}, {'ل', "l"}, {'م', "m"}, {'ن', "n"}, {'و', "v"},
        {'ه', "h"}, {'ی', "i"}, {'آ', "a"}, {' ', " "}, // و فضای خالی
        // اضافه کردن سایر حروف و نمادها در صورت نیاز
    };

    public static string ToFinglish(this string persianText)
    {
        var result = "";

        foreach (char c in persianText)
        {
            if (persianToLatinMap.TryGetValue(c, out string latinEquivalent))
            {
                result += latinEquivalent;
            }
            else
            {
                result += c; // حفظ کاراکترهایی که معادل ندارند
            }
        }

        return result.Replace(" ", "");
    }

    public static string ConvertToWords(long number)
    {
        if (number == 0)
            return "صفر";

        if (number < 0)
            return "منفی " + ConvertToWords(Math.Abs(number));

        string words = "";

        if ((number / 1000000000) > 0)
        {
            words += ConvertToWords(number / 1000000000) + " میلیارد ";
            number %= 1000000000;
        }

        if ((number / 1000000) > 0)
        {
            words += ConvertToWords(number / 1000000) + " میلیون ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += ConvertToWords(number / 1000) + " هزار ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += ConvertToWords(number / 100) + " صد ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "")
                words += "و ";

            var unitsMap = new[] { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه", "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
            var tensMap = new[] { "صفر", "ده", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += " و " + unitsMap[number % 10];
            }
        }

        return words.Trim();
    }


    public static string ToTextPrice(this long number)
    {
        long billionPart = number / 1000000000000;
        long remainder = number % 1000000000000;

        string billionPartInWords = ConvertToWords(billionPart) + " هزار میلیارد";
        string remainderInWords = ConvertToWords(remainder);

        if (remainder > 0)
            return billionPartInWords + " و " + remainderInWords;
        else
            return billionPartInWords;
    }
}

