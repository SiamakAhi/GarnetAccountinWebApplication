using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Models.Entities
{
    // انواع ماهیت محموله
    public class Cu_ConsignmentNature
    {
        [Key]
        public short Id { get; set; }

        public long SellerId { get; set; }
        [Required]
        [Display(Name = "عنوان ماهیت")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "کد ماهیت")]
        public string Code { get; set; }

        [Display(Name = "امکان حمل هوایی")]
        public bool IsAirTransportable { get; set; }

        [Display(Name = "امکان حمل زمینی")]
        public bool IsGroundTransportable { get; set; }

        [Required]
        [Display(Name = "نوع تأثیر بر نرخ پایه")]
        public short RateImpactTypeId { get; set; }

        [Display(Name = "نوع تأثیر بر نرخ پایه")]
        public Cu_RateImpactType RateImpactType { get; set; }

        [Required]
        [Display(Name = "مقدار تأثیر بر نرخ پایه")]
        public decimal RateImpactValue { get; set; }

        public virtual ICollection<Cu_Consignment> Consignments { get; set; } = new List<Cu_Consignment>();
    }
}
