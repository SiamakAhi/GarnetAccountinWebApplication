using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Organization.Models.Entities
{
    public class OrgEmployee
    {
        public int Id { get; set; } // شناسه یکتا
        public long SellerId { get; set; } // شناسه فروشنده

        [Display(Name = "شناسه شخص")]
        public long PersonId { get; set; } // شناسه شخص (از سرویس مدیریت اشخاص)

        [Display(Name = "سمت")]
        public int PositionId { get; set; } // شناسه سمت

        [Display(Name = "سمت")]
        public OrgPosition Position { get; set; } // ارتباط با سمت

        [Display(Name = "نوع رابطه")]
        public EmployeeRelationshipType RelationshipType { get; set; } // نوع رابطه با شرکت

        [Display(Name = "تاریخ شروع فعالیت")]
        public DateTime StartDate { get; set; } // تاریخ شروع فعالیت

        [Display(Name = "تاریخ پایان فعالیت")]
        public DateTime? EndDate { get; set; } // تاریخ پایان فعالیت (در صورت وجود)

        [Display(Name = "سرپرست مستقیم")]
        public int? SupervisorId { get; set; } // شناسه سرپرست مستقیم (در صورت وجود)

        [Display(Name = "سرپرست مستقیم")]
        public OrgEmployee? Supervisor { get; set; } // ارتباط با سرپرست مستقیم
    }

    public enum EmployeeRelationshipType
    {
        [Display(Name = "کارمند مستقیم شرکت")]
        DirectEmployee = 1,

        [Display(Name = "پیمانکار")]
        Contractor = 2,

        [Display(Name = "نیروی تحت امر پیمانکار")]
        SubordinateToContractor = 3,

        [Display(Name = "نماینده")]
        Representative = 4
    }
}
