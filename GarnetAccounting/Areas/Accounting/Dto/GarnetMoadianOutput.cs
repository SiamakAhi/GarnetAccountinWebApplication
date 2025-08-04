using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class GarnetMoadianOutput
    {
        [Display(Name = "شناسه")]
        public string? Identifier { get; set; }

        [Display(Name = "کد مالیاتی")]
        public string? TaxCode { get; set; }

        [Display(Name = "تاریخ")]
        public DateTime? Date { get; set; }

        [Display(Name = "شماره فاکتور")]
        public string? InvoiceNumber { get; set; }

        [Display(Name = "الگوی فاکتور")]
        public string? InvoicePattern { get; set; }

        [Display(Name = "موضوع فاکتور")]
        public string? InvoiceSubject { get; set; }

        [Display(Name = "کد مالیاتی فاکتور مرجع")]
        public string? ReferenceInvoiceTaxCode { get; set; }

        [Display(Name = "نوع فاکتور")]
        public string? InvoiceType { get; set; }

        [Display(Name = "نام خریدار")]
        public string? BuyerName { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        [Display(Name = "پرداخت نقدی")]
        public long? CashPayment { get; set; }

        [Display(Name = "قبل از تخفیف")]
        public long? AmountBeforeDiscount { get; set; }

        [Display(Name = "مجموع تخفیف")]
        public long? TotalDiscount { get; set; }

        [Display(Name = "پس از تخفیف")]
        public long? AmountAfterDiscount { get; set; }

        [Display(Name = "مجموع مالیات")]
        public long? TotalTax { get; set; }

        [Display(Name = "سایر عوارض")]
        public long? OtherCharges { get; set; }

        [Display(Name = "مجموع کل")]
        public long? TotalAmount { get; set; }

        [Display(Name = "کد پستی خریدار")]
        public string? BuyerPostalCode { get; set; }

        [Display(Name = "کد اقتصادی خریدار")]
        public string? BuyerEconomicCode { get; set; }

        [Display(Name = "کد ملی خریدار")]
        public string? BuyerNationalCode { get; set; }

        [Display(Name = "وضعیت")]
        public string? Status { get; set; }

        [Display(Name = "نوع تسویه حساب")]
        public string? SettlementType { get; set; }
    }
}
