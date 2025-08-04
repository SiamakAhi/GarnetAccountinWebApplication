using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Dto
{
    public class CostItemDto
    {
        [Key]
        public int Id { get; set; }

        public long SellerId { get; set; }

        [Required(ErrorMessage = "کد هزینه الزامی است.")]
        [Display(Name = "کد هزینه")]
        public string CostCode { get; set; }

        [Required(ErrorMessage = "شرح آیتم الزامی است.")]
        [Display(Name = "شرح آیتم")]
        public string Description { get; set; }

        [Required(ErrorMessage = "نوع محاسبه الزامی است.")]
        [Display(Name = "نوع محاسبه")]
        public short RateImpactTypeCode { get; set; }

        public string RateImpactTypeName { get; set; }

        [Required(ErrorMessage = "مقدار الزامی است.")]
        [Display(Name = "مقدار")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "مربوط به بارنامه الزامی است.")]
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
    }
}