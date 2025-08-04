using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Organization.Models.Entities
{
    public class OrgPosition
    {
        public int Id { get; set; } // شناسه یکتا
        public long SellerId { get; set; } // شناسه فروشنده

        [Display(Name = "نام سمت")]
        public string Title { get; set; } // نام سمت

        [Display(Name = "شرح سمت")]
        public string? Description { get; set; } // شرح سمت

        [Display(Name = "بخش")]
        public int DepartmentId { get; set; } // شناسه بخش مربوطه

        [Display(Name = "بخش")]
        public virtual OrgDepartment Department { get; set; } // ارتباط با بخش
    }
}
