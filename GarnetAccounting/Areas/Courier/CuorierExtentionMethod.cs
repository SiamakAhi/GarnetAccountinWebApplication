public static class CuorierExtentionMethod
{
    public static string ToBillStatusName(this Int16 statusId)
    {
        string statusName = "نامشحص";

        switch (statusId)
        {
            case 1:
                statusName = "جدید";
                break;
            case 2:
                statusName = "ارسال به پیک برای توزیع";
                break;
            case 3:
                statusName = "درحال توزیع";
                break;
            case 4:
                statusName = "تحویل گیرنده شد";
                break;
            case 6:
                statusName = "به اظهار نماینده توزیع بارنامه دارای مشکل است";
                break;
            case 7:
                statusName = "به اظهار صادرکننده بارنامه دارای مشکل است و توزیع نشود";
                break;
            case 8:
                statusName = "عودت به شعبه توزیع از پیک";
                break;
            case 9:
                statusName = "توزیع شده اما اختلاف مالی وجود دارد";
                break;
            case 10:
                statusName = "عودت به شعبه صادرکننده";
                break;
            default:
                statusName = "نامشحص";
                break;
        }
        return statusName;
    }
}


