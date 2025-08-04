using System.ComponentModel.DataAnnotations;
using GarnetAccounting.Classes.ValidationClasses;

namespace GarnetAccounting.ViewModels.CommercialViewModel
{
    public class AddSellerDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Display(Name = "شناسه سیستم حسابداری")]
        public string? AccountingSystemId { get; set; }

        [Display(Name = "حقیقی / حقوقی *")]
        [Required(ErrorMessage = "انتخاب نوع شخصیت حقیقی یا حقوقی الزامی ست")]
        public Int16 LegalStatus { get; set; }

        public Int16? TaxPayerType { get; set; }

        [Display(Name = "نام کامل انگلیسی بدون فاصله")]
        [Required(ErrorMessage = "نام انگلیسی را بدون فاصله بنویسید")]
        [EnglishNameWithoutSpace]
        public string fullNameEn { get; set; }

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
        [Required(ErrorMessage = "کد پستی 10 رقمی تعریف شده در پرونده مالیاتی خود را وارد کنید")]
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
        [Required(ErrorMessage = "فایل کلید خصوصی را انتخاب کنید")]
        public IFormFile SellerPrivateKeyFile { get; set; }

        [Display(Name = "فایل کلید عمومی *")]
        [Required(ErrorMessage = "فایل کلید عمومی را انتخاب کنید")]
        public IFormFile? SellerPublicFile { get; set; }

        [Display(Name = "فایل  CSR *")]
        [Required(ErrorMessage = "فایل CSR را وارد کنید")]
        public IFormFile SellerCSRFile { get; set; }

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
    }
}
