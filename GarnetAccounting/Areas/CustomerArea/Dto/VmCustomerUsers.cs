using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.CustomerArea.Dto
{
    public class VmCustomerUsers
    {
        public string? UserId { get; set; }

        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [Display(Name = "کاربر")]
        public string FullName { get; set; }
        [Display(Name = "نقش")]
        public string RoleName { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public string CreationDate { get; set; }
        [Display(Name = "مدیریت فروشنده")]
        public bool AllowSellerManagement { get; set; }
        [Display(Name = "مدیریت کالا/خدمت")]
        public bool AllowStuffManagement { get; set; }
        [Display(Name = "مدیریت خریداران")]
        public bool AllowBuyerManagement { get; set; }
        [Display(Name = "فروش")]
        public bool AllowSaleManagement { get; set; }
        [Display(Name = "مدیریت کاربران")]
        public bool AllowUserManagement { get; set; }
        public string[]? UserRolesArry { get; set; }
        public string Permission { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        [Display(Name = "تصویر پروفایل")]
        public string? Image { get; set; }

        [Display(Name = "جنسیت")]
        public Int16? Gender { get; set; }

        public int? PersonId { get; set; }
        public bool IsActive { get; set; }
    }
}
