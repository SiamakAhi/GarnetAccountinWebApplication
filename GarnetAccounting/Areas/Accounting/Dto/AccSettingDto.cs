using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class AccSettingDto
    {
        public long Id { get; set; }

        [Display(Name = "شناسه فروشنده")]
        public long SellerId { get; set; }

        [Display(Name = "سطح حسابداری چندمرحله‌ای")]
        public Int16? AccLevel { get; set; }

        [Display(Name = "پیش‌فرض حالت چاپ سند")]
        public Int16? DocPrintDefault { get; set; }

        [Display(Name = "نمایش تمامی حساب‌ها در سطوح تفصیلی")]
        public bool ShowAllTafsil { get; set; }

        [Display(Name = "اجباری بودن انتخاب تفصیل در تمامی سطوح")]
        public bool MandatoryTafsil { get; set; }

        [Display(Name = "چاپ نام تنظیم‌کننده سند در سند")]
        public bool PrintCreator { get; set; }

        [Display(Name = "عنوان تأییدکننده اول")]
        public string? Approver1Title { get; set; }

        [Display(Name = "نام تأییدکننده اول")]
        public string? Approver1Name { get; set; }

        [Display(Name = "عنوان تأییدکننده دوم")]
        public string? Approver2Title { get; set; }

        [Display(Name = "نام تأییدکننده دوم")]
        public string? Approver2Name { get; set; }

        [Display(Name = "حساب معین مالیات پرداختنی")]
        public int? TaxDebit { get; set; }

        [Display(Name = "حساب معین اسناد فروش")]
        public int? saleMoeinId { get; set; }
        [Display(Name = "حساب معین طرف حساب در فاکتور فروش (بدهکاران تجاری)")]
        public int? salePartyMoeinId { get; set; }

        [Display(Name = "حساب معین تخفیفات فروش")]
        public int? saleDiscountMoeinId { get; set; }

        [Display(Name = "حساب معین برگشت از فروش")]
        public int? ReturnToSaleMoeinId { get; set; }

        [Display(Name = "حساب معین ارزش افزوده فروش")]
        public int? SaleVatMoeinId { get; set; }
        public bool SetTafsilForSaleVat { get; set; } = true;

        [Display(Name = "برای اسناد فروش تفصیل خریدار درج شود")]
        public bool SetTafsilForSaleMoein { get; set; } = false;

        [Display(Name = "حساب معین اسناد خرید")]
        public int? BuyMoeinId { get; set; }
        [Display(Name = "حساب معین طرف حساب در فاکتور خرید (بستانکاران تجاری)")]
        public int? BuyPartyMoeinId { get; set; }

        [Display(Name = "حساب معین تخفیفات خرید")]
        public int? BuyDiscountMoeinId { get; set; }

        [Display(Name = "حساب معین برگشت از خرید")]
        public int? ReturnToBuyMoeinId { get; set; }

        [Display(Name = "حساب معین ارزش افزوده خرید")]
        public int? BuyVatMoeinId { get; set; }
        public bool SetTafsilForBuyVat { get; set; } = true;

        [Display(Name = "حساب معین اسناد انبار")]
        public int? WarehouseMoeinId { get; set; }

        [Display(Name = "برای فاکتورهای فروش سند اتوماتیک ثبت شود.")]
        public bool SaleIsAutoGenerate { get; set; } = false;
        [Display(Name = "برای فاکتورهای خرید سند اتوماتیک ثبت شود.")]
        public bool BuyIsAutoGenerate { get; set; } = false;
        [Display(Name = "برای اسناد انبار سند اتوماتیک ثبت شود.")]
        public bool WarehouseIsAutoGenerate { get; set; } = false;

        [Display(Name = "حساب خلاصه سود و زیان (جاری)")]
        public int? KholaseSoodVaZianMoeinId { get; set; }

        [Display(Name = "حساب معین سود و زیان انباشته")]
        public int? SoodVaZianAnbashtehMoeinId { get; set; }

        public bool IsTemprory { get; set; } = true;

        [Display(Name = "حساب معین موجودی کالا و مواد")]
        public int? InventoryMoeinId { get; set; }

        [Display(Name = "حساب معین تراز اختتامیه")]
        public int? ClosingAccountMoeinId { get; set; }

        [Display(Name = "حساب معین تراز افتتاحیه ")]
        public int? OpeningccountMoeinId { get; set; }


        // Sood/Zian Settings

        [Display(Name = "سرفصل های فروش")]
        public string[]? sz_Sale { get; set; }

        [Display(Name = "سرفصل های تخفیفات و برگشت از فروش")]
        public string[]? sz_ReturnToSale { get; set; }

        [Display(Name = "سرفصل های درآمدها")]
        public string[]? sz_Incomm { get; set; }

        [Display(Name = "سرفصل های خرید")]
        public string[]? sz_Buy { get; set; }

        [Display(Name = "سرفصل های تخفیفات و برگشت از خرید")]
        public string[]? sz_ReturnToBuy { get; set; }

        [Display(Name = "سرفصل موجودی کالا و مواد")]
        public int? sz_Inventory { get; set; }

        [Display(Name = "سرفصل های هزینه")]
        public string[]? sz_Costs { get; set; }

        [Display(Name = "نمایش هزینه ها بر اساس حساب های کل دسته بندی شوند")]
        public bool sz_ShowCostByKol { get; set; } = false;

        [Display(Name = "پایان دوره بصورت سیستمی محاسبه شود")]
        public bool sz_CalcSystemicPayanDoreh { get; set; }

    }
}
