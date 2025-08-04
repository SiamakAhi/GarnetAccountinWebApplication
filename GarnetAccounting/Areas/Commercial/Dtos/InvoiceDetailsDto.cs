using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Commercial.Dtos
{
    public class InvoiceDetailsDto
    {
        public Int64 Id { get; set; }
        public Int64 InvoiceId { get; set; }

        [Display(Name = "شماره فاکتور")]
        public string? InvoiceNumber { get; set; }

        [Display(Name = "تاریخ فاکتور")]
        public string? InvoicePersianDate { get; set; }

        [Display(Name = "خریدار")]
        public string? Buyer { get; set; }

        [Display(Name = " کالا")]
        public Int64 ProductOrServiceId { get; set; }

        [Display(Name = " شرح کالا/خدمت")]
        public string? ProductOrServiceName { get; set; }

        [Display(Name = " شناسه یکتای کالا/خدمات")]
        public string? stuffUID { get; set; }

        [Display(Name = "فی")]
        public decimal UnitPrice { get; set; } // فی برای هر واحد کالا

        [Display(Name = "مقدار")]
        public decimal Amount { get; set; } // تعداد یا مقدار کالا

        [Display(Name = "مبلغ")]
        public decimal Price { get; set; } // مبلغ کل قبل از تخفیف

        [Display(Name = "مبلغ تخفیف")]
        public decimal DiscountPrice { get; set; } // مبلغ تخفیف اعمال شده

        [Display(Name = "مبلغ کل پس از تخفیف")]
        public decimal TotalPriceAfterDiscount { get; set; } // مبلغ کل پس از تخفیف

        [Display(Name = "مبلغ مالیات و عوارض")]
        public decimal VatPrice { get; set; } // مبلغ مالیات و عوارض

        [Display(Name = "مبلغ خالص")]
        public decimal FinalPrice { get; set; } // مبلغ خالص پرداختی

        [Display(Name = "نرخ ارزش افزوده")]
        public decimal? VatRate { get; set; }
        public int? unitId { get; set; }
        public string? UnitCountName { get; set; }

        public string? TaxId { get; set; }
        public string? SettelmentType { get; set; }

    }
}
