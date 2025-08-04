using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.AvaRasta.ViewModels
{
    public class VmCustomer
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Display(Name = "نام* ")]
        [Required(ErrorMessage = "نام مشتری را وارد نمائید")]
        public string FName { get; set; }

        [Display(Name = "نام خانوادگی* ")]
        [Required(ErrorMessage = "نام خانوادگی مشتری را بنویسید")]
        public string LName { get; set; }

        [Display(Name = "نام کسب و کار*")]
        [Required(ErrorMessage = "نام شرکت یا کسب و کار مشتری الزامی است")]
        public string Title { get; set; }

        [Display(Name = "حقوقی*")]
        public bool Ishoghooghi { get; set; }

        [Display(Name = "موبایل*")]
        [Required(ErrorMessage = "شماره همراه باید نوشته شود")]
        public string Mobile { get; set; }

        [Display(Name = "تلفن ثابت")]
        public string Phone { get; set; }

        [Display(Name = "ایمیل*")]
        public string Email { get; set; }

        [Display(Name = "فکس")]
        public string? Fax { get; set; }

        [Display(Name = "شماره اقتصادی")]
        public string? EconomicNumber { get; set; }

        [Display(Name = "شماره ثبت")]
        public string? RegistrationNumber { get; set; }

        [Display(Name = "کد ملی/شناسه ملی*")]
        public string? NationalId { get; set; }

        [Display(Name = "استان")]
        public int? State { get; set; }

        [Display(Name = "شهر")]
        public int? City { get; set; }

        [Display(Name = "کد پستی")]
        public string? PostalCode { get; set; }

        [Display(Name = "آدرس")]
        public string? Address { get; set; }

        [Display(Name = "تعداد لایسنس")]
        public int LicenseCount { get; set; }

        [Display(Name = "تعداد فاکتور در سال")]
        public int InvoiceCountLimit { get; set; } = 0;

        [Display(Name = "تاریخ ثبت")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "تاریخ فعال سازی")]
        public DateTime? ActivationDate { get; set; }
        [Display(Name = "تاریخ فعال سازی")]
        public string? strActivationDate { get; set; }

        [Display(Name = "تاریخ انقضای لایسنس")]
        public string? strLicenseExpireDate { get; set; }

        [Display(Name = "تاریخ انقضای لایسنس")]
        public DateTime? LicenseExpireDate { get; set; }

        [Display(Name = "آخرین به روزرسانی")]
        public DateTime? LastUpdate { get; set; }

        [Display(Name = "کاربر ایجاد کننده")]
        public string UserCreator { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        [Display(Name = "نام کاربری")]
        public string? Username { get; set; }

        public int UserCount { get; set; } = 0;

    }
}
