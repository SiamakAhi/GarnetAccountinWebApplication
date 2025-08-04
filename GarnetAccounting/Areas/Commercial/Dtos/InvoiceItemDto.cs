using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Commercial.Dtos
{
    public class InvoiceItemDto
    {
        public Guid Id { get; set; }

        [Display(Name = "شناسه فاکتور")]
        public Guid InvoiceId { get; set; }
        public string? InvoiceNumber { get; set; }

        [Display(Name = "شناسه محصول")]
        public long ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? PakageUnitName { get; set; }
        public string? BaseUnitName { get; set; }

        [Display(Name = "تعداد کل")]
        public decimal QuantityInPakageUnit { get; set; }

        [Display(Name = "تعداد جزء")]
        public decimal QuantityInBaseUnit { get; set; }

        [Display(Name = "تعداد در هر واحد کل")]
        public decimal QuantityInPerPakage { get; set; }

        [Display(Name = "تعداد نهایی")]
        public decimal TotalQuantity { get; set; }

        [Display(Name = "قیمت واحد")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "مبلغ کل قبل از تخفیف ")]
        public decimal PriceBeForDescount { get; set; }

        [Display(Name = "تخفیف")]
        public decimal Discount { get; set; }

        [Display(Name = "جمع مبلغ بعد از تخفیف")]
        public decimal PriceAfterDiscount { get; set; }

        [Display(Name = "نرخ ارزش افزوده")]
        public decimal VatRate { get; set; }

        [Display(Name = "مبلغ ارزش افزوده")]
        public decimal VatPrice { get; set; }

        [Display(Name = "جمع فاکتور بعلاوه مبلغ ارزش افزوده")]
        public decimal FinalPrice { get; set; }

        [Display(Name = "توضیحات")]
        public string? Remarks { get; set; }

        //..
        [Display(Name = "زمان ایجاد")]
        public DateTime CreationTime { get; set; }

        [Display(Name = " کاربر ایجاد کننده")]
        public string CreatorUserId { get; set; }

        [Display(Name = "آخرین ویرایش")]
        public DateTime? LastUpdate { get; set; }

        [Display(Name = " کاربر ویرایش کننده")]
        public string? EditorUserId { get; set; }
    }
}
