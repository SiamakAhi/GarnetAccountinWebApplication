using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Models.Entities
{
    public class Acc_Setting
    {
        [Key]
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

        [Display(Name = "حساب مالیات پرداختنی")]
        public int? TaxDebit { get; set; }

        [Display(Name = "حساب معین اسناد انبار")]
        public int? WarehouseMoeinId { get; set; }

        [Display(Name = "برای فاکتورهای فروش سند اتوماتیک ثبت شود.")]
        public bool SaleIsAutoGenerate { get; set; } = false;
        [Display(Name = "برای فاکتورهای خرید سند اتوماتیک ثبت شود.")]
        public bool BuyIsAutoGenerate { get; set; } = false;
        [Display(Name = "برای اسناد انبار سند اتوماتیک ثبت شود.")]
        public bool WarehouseIsAutoGenerate { get; set; } = false;


        public int? KholaseSoodVaZianMoeinId { get; set; }
        public int? SoodVaZianAnbashtehMoeinId { get; set; }
        public bool IsTemprory { get; set; } = true;

        public int? InventoryMoeinId { get; set; }
        public int? ClosingAccountMoeinId { get; set; }
        public int? OpeningccountMoeinId { get; set; }

    }

}
