using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto.Moadian
{
    public class MoadianExportDto
    {
        [Display(Name = "کد صورتحساب در سیستم حسابداری")]
        public string? AccountingInvoiceCode { get; set; }

        [Display(Name = "شماره صورتحساب")]
        public string? InvoiceNumber { get; set; }

        [Display(Name = "تاریخ صورتحساب")]
        public string? InvoiceDate { get; set; }

        [Display(Name = "نوع صورتحساب")]
        public short? InvoiceType { get; set; }

        [Display(Name = "نام کامل خریدار")]
        public string? BuyerFullName
        {
            get
            {
                if (InvoiceType == 1)
                    return "فروش وخدمات پس از فروش مدیران خودرو";
                else
                    return "مشتری محترم";
            }
        }

        [Display(Name = "نوع شخص حقیقی یا حقوقی")]
        public short BuyerType { get; set; }

        [Display(Name = "شماره / شناسه ملی")]
        [Required]
        public string? NationalId
        {

            get
            {
                if (BuyerType == 2)
                    return "10102660590";
                else
                    return "1234567898";
            }
        }

        [Display(Name = "کد اقتصادی جدید")]
        [Required]
        public string? EconomicCode
        {
            get
            {
                if (BuyerType == 2)
                    return "10102660590";
                else
                    return "12345678980001";
            }
        }

        [Display(Name = "کدپستی")]
        public string? PostalCode { get; set; }

        [Display(Name = "آدرس")]
        public string? Address { get; set; }

        [Display(Name = "کالا / خدمت")]
        public short IsService { get; set; } = 0;

        [Display(Name = "شناسه 13 رقمی کالا یا خدمت")]
        public string? ItemIdentifier13 { get; set; } = "2710000110224";

        [Display(Name = "شرح کالا یا خدمت")]
        public string? ItemDescription { get; set; } = "قطعات";

        [Display(Name = "کد واحد اندازه گیری کالا یا خدمت")]
        public string? UnitCode { get; set; } = "1627";

        [Display(Name = "تعداد")]
        [Required]
        public long Quantity { get; set; } = 1;

        [Display(Name = "تخفیف")]
        [Required]
        public long Discount { get; set; } = 0;

        [Display(Name = "نرخ ارزش افزوده")]
        [Required]
        public float VATRate { get; set; } = 10;

        [Display(Name = "مبلغ ارزش افزوده")]
        [Required]
        public long VATAmount { get; set; } = 0;

        [Display(Name = "نوع تسویه حساب")]
        public short SettlementType { get; set; } = 1;

        public long BasePrice { get; set; } = 0;
        public long TotalCost { get; set; }
        public long TotalPriceBeforDiscount => BasePrice;
        public long TotalDiscount { get; set; } = 0;
        public long TotalPriceAfterDiscount => TotalPriceBeforDiscount - TotalDiscount;
        public long VatPrice => (long)TotalPriceAfterDiscount * 10 / 100;
        public long UnitPrice => TotalPriceAfterDiscount + VatPrice;

    }
}
