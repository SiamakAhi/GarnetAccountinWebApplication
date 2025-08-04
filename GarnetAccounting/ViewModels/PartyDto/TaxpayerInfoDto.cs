using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.ViewModels.PartyDto
{
    public class TaxpayerInfoDto
    {
        public int Id { get; set; }

        public long SellerId { get; set; }
        public string? SellerName { get; set; }

        [Display(Name = "شناسه مالیاتی")]
        public string? TaxId { get; set; }

        [Display(Name = "شناسه/کد ملی")]
        public string? NationalCode { get; set; }

        [Display(Name = "کد پستی گواهی شده")]
        public string? PostalCode { get; set; }

        [Display(Name = "شماره ثبت")]
        public string? RegistrationNumber { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime? RegistrationDate { get; set; }

        [Display(Name = "آدرس ثبتی شرکت/یا شخص حقیقی")]
        public string? RegisteredAddress { get; set; }

        [Display(Name = "موضوع شرکت")]
        public string? CompanySubject { get; set; }

        [Display(Name = "فعالیت شرکت")]
        public string? CompanyActivity { get; set; }

        [Display(Name = "نوع شرکت")]
        public string? CompanyType { get; set; }

        [Display(Name = "وضعیت ارزش افزوده")]
        public string? VATStatus { get; set; }

        [Display(Name = "نوع اظهارنامه")]
        public string? DeclarationType { get; set; }

        [Display(Name = "نام مدیرعامل")]
        public string? CEOName { get; set; }

        [Display(Name = "کد ملی مدیرعامل")]
        public string? CEONationalCode { get; set; }

        [Display(Name = "تلفن مدیرعامل")]
        public string? CEOPhone { get; set; }

        [Display(Name = "آدرس مدیرعامل")]
        public string? CEOAddress { get; set; }

        [Display(Name = "نام عضو هیئت مدیره 1")]
        public string? BoardMember1Name { get; set; }

        [Display(Name = "کدملی عضو هیئت مدیره 1")]
        public string? BoardMember1NationalCode { get; set; }

        [Display(Name = "تلفن عضو هیئت مدیره 1")]
        public string? BoardMember1Phone { get; set; }

        [Display(Name = "آدرس عضو هیئت مدیره 1")]
        public string? BoardMember1Address { get; set; }

        [Display(Name = "نام عضو هیئت مدیره 2")]
        public string? BoardMember2Name { get; set; }

        [Display(Name = "کد ملی عضو هیئت مدیره 2")]
        public string? BoardMember2NationalCode { get; set; }

        [Display(Name = "تلفن عضو هیئت مدیره 2")]
        public string? BoardMember2Phone { get; set; }

        [Display(Name = "آدرس عضو هیئت مدیره 2")]
        public string? BoardMember2Address { get; set; }

        [Display(Name = "نام عضو هیئت مدیره 3")]
        public string? BoardMember3Name { get; set; }

        [Display(Name = "کد ملی عضو هیئت مدیره 3")]
        public string? BoardMember3NationalCode { get; set; }

        [Display(Name = "تلفن عضو هیئت مدیره 3")]
        public string? BoardMember3Phone { get; set; }

        [Display(Name = "آدرس عضو هیئت مدیره 3")]
        public string? BoardMember3Address { get; set; }

        [Display(Name = "کد واحد مالیاتی")]
        public string? TaxUnitCode { get; set; }

        [Display(Name = "آدرس حوزه مالیاتی")]
        public string? TaxOfficeAddress { get; set; }

        [Display(Name = "رییس گروه مالیاتی")]
        public string? TaxGroupHead { get; set; }

        [Display(Name = "ممیز مالیاتی")]
        public string? TaxAuditor { get; set; }

        [Display(Name = "سرممیز مالیاتی")]
        public string? SeniorTaxAuditor { get; set; }

        [Display(Name = "ممیز کل مالیاتی")]
        public string? ChiefTaxAuditor { get; set; }

        [Display(Name = "کد رهگیری پیش ثبت نام")]
        public string? PreRegistrationTrackingCode { get; set; }

        [Display(Name = "شماره پرونده مالیاتی")]
        public string? TaxFileNumber { get; set; }

        [Display(Name = "شناسه یکتا حافظه مالیاتی")]
        public string? UniqueTaxMemoryId { get; set; }

        [Display(Name = "کلمه عبور پنل دارایی")]
        public string? AssetPanelPassword { get; set; }

        [Display(Name = "نام نرم افزار حسابداری")]
        public string? AccountingSoftwareName { get; set; }

        [Display(Name = "رمز عبور نرم افزار حسابداری")]
        public string? AccountingSoftwarePassword { get; set; }

        [Display(Name = "نام نرم افزار واسط سامانه مودیان")]
        public string? InterfaceSoftwareName { get; set; }

        [Display(Name = "رمز عبور نرم افزار واسط")]
        public string? InterfaceSoftwarePassword { get; set; }

        [Display(Name = "نام مدیرمالی شرکت")]
        public string? CFOName { get; set; }

        [Display(Name = "تلفن همراه مدیرمالی")]
        public string? CFOMobile { get; set; }

        [Display(Name = "نام مشاور مالی")]
        public string? FinancialAdvisorName { get; set; }

        [Display(Name = "شماره همراه مشاور")]
        public string? FinancialAdvisorMobile { get; set; }

        [Display(Name = "نام حسابدار")]
        public string? AccountantName { get; set; }

        [Display(Name = "شماره همراه حسابدار")]
        public string? AccountantMobile { get; set; }

        [Display(Name = "نام موسسه حسابرسی")]
        public string? AuditFirmName { get; set; }
    }
}
