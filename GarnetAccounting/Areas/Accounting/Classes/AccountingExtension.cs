public static class AccountingExtension
{
    public static string AccToGroupTypeName(this short typeId)
    {
        string name = "";
        switch (typeId)
        {
            case 1:
                name = "ترازنامه ای - دائم";
                break;
            case 2:
                name = "سود و زیانی - موقت";
                break;
            case 3:
                name = "کنترلی و اماری";
                break;
            default:
                name = "تعریف نشده";
                break;
        }

        return name;
    }
    public static string AccToGroupType(this short typeId)
    {
        string name = "";
        switch (typeId)
        {
            case 1:
                name = "دارایی";
                break;
            case 2:
                name = "بدهی";
                break;
            case 3:
                name = "سرمایه";
                break;
            case 4:
                name = "درآمد";
                break;
            case 5:
                name = "هزینه";
                break;
            case 6:
                name = "انتظامی";
                break;
            default:
                name = "تعریف نشده";
                break;
        }

        return name;
    }
    public static string AccToNatureName(this short NatureId)
    {
        string name = "";
        switch (NatureId)
        {
            case 1:
                name = "بدهکار";
                break;
            case 2:
                name = "بستانکار";
                break;
            case 3:
                name = "شناور";
                break;
            default:
                name = "";
                break;
        }

        return name;
    }
    public static string AccToBalanceNatureName(this short NatureId)
    {
        string name = "";
        switch (NatureId)
        {
            case 1:
                name = "بدهکار";
                break;
            case 2:
                name = "بستانکار";
                break;
            case 3:
                name = "";
                break;
            default:
                name = "";
                break;
        }

        return name;
    }
    public static string AccToDocStatusName(this short DocStatusId)
    {
        string name = "";
        switch (DocStatusId)
        {
            case 1:
                name = "یادداشت";
                break;
            case 2:
                name = "ثبت موقت";
                break;
            case 3:
                name = "ثبت دائم";
                break;
            default:
                name = "";
                break;
        }

        return name;
    }
    public static string AccToDoctypeName(this short DocTypeId)
    {
        string name = "";
        switch (DocTypeId)
        {
            case 1:
                name = "سند روزانه";
                break;
            case 2:
                name = "سند افتتاحیه";
                break;
            case 3:
                name = "سند اختتامیه";
                break;
            case 4:
                name = "بابت بستن حسابهای موقت";
                break;
            case 5:
                name = "بابت بستن حسابهای دائم";
                break;
            case 6:
                name = "بابت طبقه بندی حسابها";
                break;
            default:
                name = "";
                break;
        }

        return name;
    }

    // Tax Payer Systems (Moadian)

    public static short? Moadian_IncoiceSubjectToCode(this string InvoiceSubject)
    {
        short? SubjectCode = null;
        switch (InvoiceSubject)
        {
            case "اصلی":
                SubjectCode = 1;
                break;
            case "اصلاحی":
                SubjectCode = 2;
                break;
            case "برگشت از فروش":
                SubjectCode = 3;
                break;
            case "ابطالی":
                SubjectCode = 4;
                break;

            default:
                break;
        }

        return SubjectCode;
    }

    public static short? Moadian_IncoiceStatusToCode(this string InvoiceStatus)
    {
        short? InvoiceStatusCode = null;
        switch (InvoiceStatus)
        {
            case "تایید شده":
                InvoiceStatusCode = 1;
                break;
            case "تایید سیستمی":
                InvoiceStatusCode = 2;
                break;
            case "در انتظار واکنش":
                InvoiceStatusCode = 3;
                break;
            case "عدم نیاز به واکنش":
                InvoiceStatusCode = 4;
                break;
            case "باطل شده":
                InvoiceStatusCode = 5;
                break;
            case "رد شده":
                InvoiceStatusCode = 6;
                break;
            case "عدم امکان واکنش":
                InvoiceStatusCode = 7;
                break;

            default:
                break;
        }

        return InvoiceStatusCode;
    }
}

