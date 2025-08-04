using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Models.Entities
{
    public class Cu_Service
    {
        [Key]
        public int Id { get; set; }

        public long SellerId { get; set; }

        [Display(Name = "کد سرویس")]
        [Required(ErrorMessage = "فیلد کد سرویس الزامی است")]
        public string ServiceCode { get; set; }

        [Display(Name = "نام سرویس")]
        [Required(ErrorMessage = "فیلد نام الزامی است")]
        public string ServiceName { get; set; }

        [Display(Name = "نام انگلیسی سرویس")]
        [Required(ErrorMessage = "فیلد نام الزامی است")]
        public string? ServiceName_En { get; set; }

        [Display(Name = "درصد تاثیر بر نرخ")]
        public decimal ServicePercentage { get; set; }

        public Int16 ShipmentTypeCode { get; set; }

        [MaxLength(10)]
        public string RatingType { get; set; } = "cr";

        public virtual ICollection<Cu_BillOfLading> BillOfLadings { get; set; } = new List<Cu_BillOfLading>();
    }
}
