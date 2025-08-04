using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.CustomerArea.Dto
{
    public class AddCustomerUserDto
    {
        public string? Id { get; set; }

        [Display(Name = "نام کاربری *")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "نام *")]
        public string? FName { get; set; }

        [Display(Name = "نام خانوادگی *")]
        public string? LName { get; set; }

        [Display(Name = "موبایل *")]
        public string? Mobile { get; set; }

        [Display(Name = "ایمیل *")]
        [EmailAddress]
        public string? email { get; set; }

        [Required]
        [Display(Name = "رمز عبور *")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "تکرار رمز عبور *")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Display(Name = "جنسیت *")]
        public short? Gender { get; set; }
        public int customerId { get; set; }

        [Display(Name = "انتخاب فروشنده(گان) ")]
        public Int64[]? SellersId { get; set; }

        [Display(Name = "دسترسی به مدیریت اطلاعات فروشنده(گان)")]
        public bool AllowSellerManagement { get; set; }
        [Display(Name = "دسترسی به مدیریت اطلاعات کالا و خدمات")]
        public bool AllowStuffManagement { get; set; }
        [Display(Name = "دسترسی به مدیریت اطلاعات خریداران")]
        public bool AllowBuyerManagement { get; set; }

        [Display(Name = "دسترسی به ثبت و مدیریت صورتحساب های فروش")]
        public bool AllowSaleManagement { get; set; }
        [Display(Name = "مدیریت کاربران")]
        public bool AllowUserManagement { get; set; }
    }
}
