using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Models.Entities
{
    public class Cu_BillCost
    {
        [Key]
        public long Id { get; set; }  // شناسه یکتا برای هزینه
        public long SellerId { get; set; }

        [Display(Name = "شناسه بارنامه")]
        [Required(ErrorMessage = "بارنامه شناسایی نشد")]
        public Guid BillOfLadingId { get; set; }
        public virtual Cu_BillOfLading BillOfLading { get; set; }

        [Display(Name = "شناسه مرسوله")]
        public Guid? ConsignmentId { get; set; }
        public virtual Cu_Consignment? Consignment { get; set; }

        [Display(Name = "نوع هزینه")]
        [Required(ErrorMessage = "شرح هزینه")]
        public int CostTypeId { get; set; }
        public Cu_BillOfLadingCostItem CostType { get; set; }

        [Display(Name = "مبلغ هزینه")]
        [Required(ErrorMessage = "مبلغ هزینه الزامی است")]
        public long Amount { get; set; }

        [Display(Name = "تاریخ ثبت هزینه")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
