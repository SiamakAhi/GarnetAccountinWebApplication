using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Dto
{
    public class AddBranchUserDto
    {
        [Required]
        public Guid DepartmentUserId { get; set; }
        public short DepartmentCode { get; set; }
        public long SellerId { get; set; }
        [Required(ErrorMessage = "شناسه شعبه معتبر نیست")]
        public Guid BranchId { get; set; }
        public bool IsSupervisor { get; set; }

        [Display(Name = "نام ")]
        [Required(ErrorMessage = "ورود نام الزامی است")]
        public string? FName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "ورود نام خانوادگی الزامی است")]
        public string? LName { get; set; }

        [Display(Name = "موبایل")]
        public string? Mobile { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "ورود ایمیل معتبر الزامی است")]
        [EmailAddress(ErrorMessage = "ایمل وارد شده معتبر نیست")]
        public string? email { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "ورود نام کاربری الزامی است")]
        public string UserName { get; set; }

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
        public int? CustomerId { get; set; }

    }
}
