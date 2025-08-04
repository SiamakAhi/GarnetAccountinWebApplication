using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Warehouse.Models.Entities
{
    public class Wh_ProductUnit
    {
        public long ProductUnitId { get; set; }

        [Display(Name = "شناسه محصول")]
        public long ProductId { get; set; }

        [Display(Name = "شناسه واحد اندازه‌گیری")]
        public int UnitOfMeasureId { get; set; }

        [Display(Name = "نوع واحد")]
        public Int16 UnitType { get; set; }

        [Display(Name = "ضریب تبدیل")]
        public decimal ConversionFactor { get; set; }

        // Navigation Properties
        public virtual Wh_Product Product { get; set; }
        public virtual Wh_UnitOfMeasure UnitOfMeasure { get; set; }
    }

}
