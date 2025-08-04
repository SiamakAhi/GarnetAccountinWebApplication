using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Models.Entities
{
    // انواع آیتم‌های ریالی بارنامه
    public class Cu_BillOfLadingCostItem
    {
        [Key]
        public int Id { get; set; }
        public long SellerId { get; set; }

        [Required]
        [Display(Name = "کد هزینه")]
        public string CostCode { get; set; }

        [Required]
        [Display(Name = "شرح آیتم")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "نوع محاسبه")]
        public short RateImpactTypeCode { get; set; }

        [Required]
        [Display(Name = "مقدار")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "مربوط به بارنامه")]
        public bool ForBillOfLading { get; set; }

        [Display(Name = "مربوط به مرسوله")]
        public bool ForConsignment { get; set; }

        [Display(Name = "افزودن اتوماتیک")]
        public bool IsAutoAdded { get; set; } = false;

        [Display(Name = "شناسه حساب معین")]
        public int? AccountMoeinId { get; set; }

        [Display(Name = "شناسه حساب تفصیلی")]
        public long? AccountTafsilId { get; set; }

        public virtual ICollection<Cu_BillCost> BillCosts { get; set; }
    }
}
