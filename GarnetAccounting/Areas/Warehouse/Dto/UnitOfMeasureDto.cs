using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Warehouse.Dto
{
    public class UnitOfMeasureDto
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "شناسه فروشنده")]
        public long SellerId { get; set; }

        [Display(Name = "نام واحد")]
        public string UnitName { get; set; }

        [Display(Name = "کد واحد")]
        public string? UnitCode { get; set; }

        [Display(Name = "نماد واحد")]
        public string? UnitSymbol { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; }
    }
}
