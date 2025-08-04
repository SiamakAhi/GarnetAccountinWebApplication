using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.ViewModels.IdentityViewModels
{
    public class VmRegisterUser
    {
        public string? Id { get; set; }

        [Display(Name = "نام کاربری")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "نام ")]
        public string? FName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string? LName { get; set; }

        [Display(Name = "موبایل")]
        public string? Mobile { get; set; }

        [Display(Name = "ایمیل")]
        [EmailAddress]
        public string? email { get; set; }

        [Required]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "تکرار رمز عبور")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Display(Name = "نقش ها")]
        public string[]? Roles { get; set; }

        [Display(Name = "جنسیت")]
        public short? Gender { get; set; }

        [Display(Name = "طرف قرارداد")]
        public int? customerId { get; set; }

        [Display(Name = "شناسه مشتری")]
        public int? personId { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
