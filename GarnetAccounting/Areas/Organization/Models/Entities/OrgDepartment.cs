using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Organization.Models.Entities
{
    public class OrgDepartment
    {
        public int Id { get; set; } // شناسه یکتا
        public long SellerId { get; set; }

        [Display(Name = "نام بخش")]
        public string Name { get; set; } // نام بخش یا واحد

        [Display(Name = "کد بخش")]
        public string Code { get; set; } // کد بخش برای شناسایی

        [Display(Name = "بخش والد")]
        public int? ParentDepartmentId { get; set; } // بخش والد (در صورت وجود)

        [Display(Name = "بخش والد")]
        public OrgDepartment? ParentDepartment { get; set; } // ارتباط با بخش والد

        [Display(Name = "زیربخش‌ها")]
        public ICollection<OrgDepartment>? ChildDepartments { get; set; } // زیربخش‌ها

        public ICollection<OrgPosition>? Positions { get; set; } //سمت ها
    }
}
