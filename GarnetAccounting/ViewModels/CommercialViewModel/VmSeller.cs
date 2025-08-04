using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.ViewModels.CommercialViewModel
{
    public class VmSeller
    {
        public Int64 Id { get; set; }

        [Display(Name = "مشتری")]
        public int CustomerId { get; set; }

        [Display(Name = "حقیقی/حقوقی")]
        public Int16 LegalStatus { get; set; }

        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "نام کامل به انگلیسی . بدون فاصله")]
        public string fullNameEn { get; set; }

        [Display(Name = "کد پستی")]
        public string? PostalCode { get; set; }

        [Display(Name = "شناسه/کد ملی *")]
        public string NationalId { get; set; }

        [Display(Name = "کد اقتصادی *")]
        public string? EconomicCode { get; set; }

        [Display(Name = "کد حافظه مالیاتی *")]
        public string? TaxMemoryId { get; set; }

        [Display(Name = "شماره ثبت")]
        public string? RegistrationNumber { get; set; }

        [Display(Name = "لوگو")]
        public string? Logo { get; set; }

        [Display(Name = "استان")]
        public string? Province { get; set; }

        [Display(Name = "شهر")]
        public string? City { get; set; }

        [Display(Name = "نشانی")]
        public string? Address { get; set; }

        [Display(Name = "کلبد عمومی (Public Key)")]
        public string? SellerPublicKey { get; set; }

        [Display(Name = "کلید خصوصی (Private Key) *")]

        public string? SellerPrivateKey { get; set; }

        [Display(Name = "CSR Key")]
        public string? SellerCSRKey { get; set; }

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
