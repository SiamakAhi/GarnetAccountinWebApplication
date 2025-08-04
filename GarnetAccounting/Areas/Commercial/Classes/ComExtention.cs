
public static class ComExtention
{
    public static string com_InvoiceTypeName(this short typeId)
    {
        string name = "";
        switch (typeId)
        {
            case 1:
                name = "خرید";
                break;
            case 2:
                name = "فروش";
                break;
            case 3:
                name = "صورت وضعیت";
                break;
            case 4:
                name = "پیش فاکتور";
                break;
            default:
                break;
        }
        return name;
    }

    public static string com_ToStatusName(this short statusId)
    {
        string name = "";
        switch (statusId)
        {
            case 1:
                name = "یادداشت";
                break;
            case 2:
                name = "جدید";
                break;
            case 3:
                name = "تسویه";
                break;
            case 4:
                name = "ابطال شده";
                break;
            case 5:
                name = "ابطال شده";
                break;
            default:
                break;
        }
        return name;
    }

    public static string com_ToSettelmentTypeName(this int id)
    {
        string name = "";
        switch (id)
        {
            case 1:
                name = "نقد";
                break;
            case 2:
                name = "نسیه";
                break;
            case 3:
                name = "اعتباری";
                break;
            case 4:
                name = "نقد و نسیه";
                break;
            case 5:
                name = "افساط";
                break;
            default:
                break;
        }
        return name;
    }

    public static string ToLegalStatusName(this short legalStatus)
    {
        string name = "";
        switch (legalStatus)
        {
            case 1:
                name = "حقیقی";
                break;
            case 2:
                name = "حقوقی";
                break;
            case 3:
                name = "مشارکت مدنی";
                break;
            default:
                name = "";
                break;
        }

        return name;
    }
}

