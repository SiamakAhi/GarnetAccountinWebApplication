using GarnetAccounting;
using Newtonsoft.Json;


public static class AppExtension
{

    public static string ToWhDocTypeName(this Int16 typeId)
    {
        string str = "";

        switch (typeId)
        {
            case 1:
                str = "رسید دائم";
                break;
            case 2:
                str = "رسید موقت";
                break;
            case 3:
                str = "رسید کالای تعمیراتی";
                break;
            case 4:
                str = "رسید کالای امانی";
                break;
            case 5:
                str = "حواله خروج";
                break;
            case 6:
                str = "حواله کالای تعمیراتی";
                break;
            case 7:
                str = "رسید انتقال";
                break;
            case 8:
                str = "حواله انتقال";
                break;
            case 9:
                str = "موجودی اول دوره";
                break;
            default:
                break;
        }

        return str;
    }

    public static Int16 ToWhDocTypeOperation(this Int16 typeId)
    {
        Int16 str = 0;

        switch (typeId)
        {
            case 1:
                str = 1;
                break;
            case 2:
                str = 0;
                break;
            case 3:
                str = 3;
                break;
            case 4:
                str = 3;
                break;
            case 5:
                str = 2;
                break;
            case 6:
                str = 4;
                break;
            case 7:
                str = 1;
                break;
            case 8:
                str = 2;
                break;
            case 9:
                str = 1;
                break;
            default:
                break;
        }

        return str;
    }

    public static string SettelmentName(this Int16 typeId)
    {
        string str = "نامشخص";

        switch (typeId)
        {
            case 1:
                str = "نقد";
                break;
            case 2:
                str = "افساط";
                break;
            case 3:
                str = "اعتباری";
                break;
            default:
                break;
        }

        return str;
    }
    public static string PaymentMethodName(this Int16 typeId)
    {
        string str = "نامشخص";

        switch (typeId)
        {
            case 1:
                str = "نقد";
                break;
            case 2:
                str = "واریز";
                break;
            case 3:
                str = "چک";
                break;
            case 4:
                str = "پرداخت از محل بستانکاری";
                break;
            default:
                break;
        }

        return str;
    }

    public static string TransactionName(this Int16 typeId)
    {
        string str = "نامشخص";
        //..
        //1- بدهی بابت فاکتور فروش
        //2- کارمزد اقساط 
        //3- جریمه عدم پرداخت قسط
        //4- برگشت پیش دریافت

        //..........

        //20 To 39 -Bes
        //..
        //20- واریز بابت فاکتور فروش
        //21- واریز قسط
        //22- برگشت از فروش
        //23- پیش دریافت خرید کالا
        switch (typeId)
        {
            case 1:
                str = "بدهی بابت فاکتور فروش";
                break;
            case 2:
                str = "کارمزد اقساط";
                break;
            case 3:
                str = "برگشت از فروش";
                break;
            case 4:
                str = "برگشت پیش دریافت";
                break;
            case 20:
                str = "واریز بابت فاکتور فروش";
                break;
            case 21:
                str = "واریز قسط";
                break;
            case 22:
                str = "برگشت از فروش";
                break;
            case 23:
                str = "پیش دریافت خرید کالا";
                break;
            case 24:
                str = "تسویه اقساط";
                break;
            default:
                break;
        }

        return str;
    }

    public static string ToJsonResult(this clsResult result)
    {
        string res = JsonConvert.SerializeObject(result);
        return res;
    }

    public static string CreatJsonResult(this bool success, string message = "", string returnUrl = "")
    {
        clsResult result = new clsResult
        {
            Message = message,
            returnUrl = returnUrl,
            Success = success,
        };
        string res = JsonConvert.SerializeObject(result);
        return res;
    }

    public static string ToSubsystemPersianName(this string EnName)
    {
        string str = "نامشخص";

        switch (EnName)
        {
            case "Accounting":
                str = "حسابداری";
                break;
            case "Khazaneh":
                str = "خزانه";
                break;
            case "Sale":
                str = "فروش";
                break;
            case "Buy":
                str = "خرید";
                break;
            case "Warehouse":
                str = "انبار";
                break;
            case "Contract":
                str = "قراردادها";
                break;
            default:
                break;
        }

        return str;
    }
}

