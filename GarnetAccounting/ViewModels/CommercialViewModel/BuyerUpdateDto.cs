using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.ViewModels.CommercialViewModel
{
    public class BuyerUpdateDto
    {
        [Required]
        public Int64 Id { get; set; }
        [Required]
        public int CustomerId { get; set; }

        [Display(Name = "شناسه سیستم حسابداری")]
        public string? AccountingSystemId { get; set; }

        [Display(Name = "حقیقی / حقوقی *")]
        [Required(ErrorMessage = "انتخاب نوع شخصیت حقیقی یا حقوقی الزامی ست")]
        public Int16 LegalStatus { get; set; }

        public Int16? TaxPayerType { get; set; }

        [Display(Name = "نام شخص حقیقی یا حقوغی *")]
        [Required(ErrorMessage = "نوشتن نام الزامیست")]
        public string Name { get; set; }

        [Display(Name = "شناسه/کد ملی *")]
        // [Required(ErrorMessage = "شناسه / کد ملی نامعتبر است")]
        public string? NationalId { get; set; }

        [Display(Name = "کد اقتصادی *")]
        // [Required(ErrorMessage = "کد اقتصادی جدید خود را وارد نمایید")]
        public string? EconomicCode { get; set; }

        [Display(Name = "شماره ثبت")]
        public string? RegistrationNumber { get; set; }

        [Display(Name = "استان")]
        public string? Province { get; set; }

        [Display(Name = "شهر")]
        public string? City { get; set; }

        [Display(Name = "نشانی")]
        public string? Address { get; set; }

        [Display(Name = "کد پستی 10 رقمی *")]
        // [Required(ErrorMessage = "کد پستی 10 رقمی تعریف شده در پرونده مالیاتی")]
        public string? PostalCode { get; set; }

        [Display(Name = " موبایل")]
        public string? MobilePhone { get; set; }

        [Display(Name = "نمابر")]
        public string? Fax { get; set; }

        [Display(Name = "توضیحات ")]
        public string? InvoiceDescription { get; set; }
    }
}
