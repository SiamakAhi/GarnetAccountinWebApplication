using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.ViewModels.CommercialViewModel
{
    public class UpdateSellerDto
    {
        public Int64 Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Display(Name = "شناسه سیستم حسابداری")]
        public string? AccountingSystemId { get; set; }

        [Display(Name = "حقیقی / حقوقی *")]
        [Required(ErrorMessage = "انتخاب نوع شخصیت حقیقی یا حقوقی الزامی ست")]
        public Int16 LegalStatus { get; set; }

        public Int16? TaxPayerType { get; set; }

        [Display(Name = "نام کامل انگلیسی بدون فاصله")]
        public string? fullNameEn { get; set; }

        [Display(Name = "نام شخص حقیقی یا حقوغی *")]
        [Required(ErrorMessage = "نوشتن نام الزامیست")]
        public string Name { get; set; }


        [Display(Name = "شناسه/کد ملی *")]
        [Required(ErrorMessage = "شناسه / کد ملی نامعتبر است")]
        public string NationalId { get; set; }

        [Display(Name = "کد اقتصادی *")]
        //[Required(ErrorMessage = "کد اقتصادی جدید خود را وارد نمایید")]
        public string EconomicCode { get; set; }


        [Display(Name = "کد حافظه مالیاتی *")]
        //[Required(ErrorMessage = "شناسه حافظه مالیاتی خود را از کارپوشه مالیاتی خود دریافت و وارد نمایید")]
        public string? TaxMemoryId { get; set; }


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

        [Display(Name = "Public Key")]
        public string? SellerPublicKey { get; set; }

        [Display(Name = "کلید خصوصی *")]

        public string? SellerPrivateKey { get; set; }

        [Display(Name = "فایل کلید خصوصی *")]
        public IFormFile? SellerPrivateKeyFile { get; set; }

        [Display(Name = "فایل کلید عمومی *")]
        public IFormFile? SellerPublicFile { get; set; }

        [Display(Name = "فایل  CSR *")]
        public IFormFile? SellerCSRFile { get; set; }


        [Display(Name = "CSR Key")]
        public string? SellerCSRKey { get; set; }

        [Display(Name = "لوگو")]
        public string? Logo { get; set; }

        [Display(Name = "لوگو")]
        public IFormFile? LogoFile { get; set; }

        [Display(Name = "آیا لوگو در فاکتور نمایش داده شود؟")]
        public bool IsLogoDisplayedOnInvoice { get; set; } = true;

        [Display(Name = "توضیحات فاکتور")]
        public string? InvoiceDescription { get; set; }

        [Display(Name = "نام مدیرعامل")]
        public string? CEOName { get; set; }

        [Display(Name = "شماره تماس مدیرعامل")]
        public string? CEOContactNumber { get; set; }

        [Display(Name = "شماره پرونده مالیاتی")]
        public string? TaxFileNumber { get; set; }

        [Display(Name = "کد واحد مالیاتی")]
        public string? TaxUnitCode { get; set; }

        [Display(Name = "آدرس واحد مالیاتی")]
        public string? TaxUnitAddress { get; set; }

        [Display(Name = "ممیز مالیاتی")]
        public string? TaxAuditor { get; set; }

        [Display(Name = "رمز عبور پنل مالیاتی")]
        public string? TaxPanelPassword { get; set; }

        [Display(Name = "شماره پیگیری ثبت نام")]
        public string? TaxTrackingNumber { get; set; }

    }
}
