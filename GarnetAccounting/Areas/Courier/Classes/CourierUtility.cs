
public static class CourierUtility
{

    public static string ToShipmentTypeName(this Int16 ShipmentCode)
    {
        string shipmentName = "";

        switch (ShipmentCode)
        {
            case 1:
                shipmentName = "زمینی";
                break;
            case 2:
                shipmentName = "هوایی";
                break;
            case 3:
                shipmentName = "دریایی";
                break;

            default:
                break;
        }

        return shipmentName;
    }

    public static string ToRateImpactType(this Int16 InpactTypeId)
    {
        string shipmentName = "";

        switch (InpactTypeId)
        {
            case 1:
                shipmentName = "ثابت";
                break;
            case 2:
                shipmentName = "درصداز کل بارنامه";
                break;
            case 3:
                shipmentName = "درصد از مبلغ حمل بار";
                break;
            case 4:
                shipmentName = "محاسبه توسط کاربر";
                break;
            case 5:
                shipmentName = "محاسبه سیستمی";
                break;

            default:
                break;
        }

        return shipmentName;
    }

    //وضعیت بارنامه
    public static string ToBillStatusNameType(this short InpactTypeId)
    {
        string shipmentName = "وضعیت نامشخص";

        switch (InpactTypeId)
        {
            case 1:
                shipmentName = "درحال صدور";
                break;
            case 2:
                shipmentName = "در انتظار جمع آوری";
                break;
            case 3:
                shipmentName = "ورود به هاب مبدأ";
                break;
            case 4:
                shipmentName = "در حال رهسپاری";
                break;
            case 5:
                shipmentName = "ورود به هاب مقصد";
                break;
            case 6:
                shipmentName = "تحویل توزیع کننده شد";
                break;
            case 7:
                shipmentName = "تحویل شد";
                break;

            default:
                break;
        }

        return shipmentName;
    }

}

