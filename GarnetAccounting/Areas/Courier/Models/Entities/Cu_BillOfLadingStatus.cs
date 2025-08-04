using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Models.Entities
{
    // انواع وضعیت بارنامه
    public class Cu_BillOfLadingStatus
    {
        [Key]
        public short Id { get; set; }

        [Required]
        [Display(Name = "عنوان وضعیت")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "کد وضعیت")]
        public string Code { get; set; }

        [Display(Name = "ارسال نوتیفیکیشن به مشتری")]
        public bool SendNotificationToCustomer { get; set; }

        [Display(Name = "ارسال نوتیفیکیشن به عملیات")]
        public bool SendNotificationToOperations { get; set; }

        public virtual ICollection<Cu_BillOfLading> BillOfLadings { get; set; } = new List<Cu_BillOfLading>();
    }
}
