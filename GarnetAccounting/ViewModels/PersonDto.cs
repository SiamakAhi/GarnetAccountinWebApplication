using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.ViewModels
{
    public class PersonDto
    {
        public long Id { get; set; }
        public int CustomerId { get; set; }

        [Display(Name = "شناسه سیستم حسابداری")]
        public string? AccountingSystemId { get; set; }

        [Required(ErrorMessage = "فروشنده نامشخص است.")]
        public Int64? SellerId { get; set; }

        [Display(Name = "نوع شخص *")]
        [Required(ErrorMessage = "انتخاب نوع خریدار الزامی ست")]
        public Int16 LegalStatus { get; set; }

        [Display(Name = "نام شخص حقیقی یا حقوغی *")]
        [Required(ErrorMessage = "نوشتن نام الزامیست")]
        public string Name { get; set; }

        [Display(Name = "شناسه/کد ملی *")]
        public string? NationalId { get; set; }

        [Display(Name = "کد اقتصادی *")]
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
        public string? PostalCode { get; set; }

        [Display(Name = " موبایل")]
        public string? MobilePhone { get; set; }

        [Display(Name = "نمابر")]
        public string? Fax { get; set; }

        public bool? IsCustomer { get; set; }
        public bool? IsVendor { get; set; }
        public long? TafsilId { get; set; }
        public string? TafsilCode { get; set; }
    }
}
