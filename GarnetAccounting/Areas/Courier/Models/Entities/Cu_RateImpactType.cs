using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Models.Entities
{
    // انواع تأثیر بر نرخ پایه
    public class Cu_RateImpactType
    {
        [Key]
        public short Id { get; set; }

        [Display(Name = "کد تأثیر")]
        public Int16 RateImpactTypeCode { get; set; }

        [Required]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        public virtual ICollection<Cu_ConsignmentNature> ConsignmentNatures { get; set; }
    }
}
