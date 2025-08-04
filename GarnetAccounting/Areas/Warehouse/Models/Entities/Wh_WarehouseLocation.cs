using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Warehouse.Models.Entities
{
    public class Wh_WarehouseLocation
    {
        public long LocationId { get; set; }

        [Display(Name = "شناسه فروشنده")]
        public long SellerId { get; set; }

        [Display(Name = "شناسه انبار")]
        public long WarehouseId { get; set; }

        [Display(Name = "کد مکان")]
        public string LocationCode { get; set; }

        [Display(Name = "نام مکان")]
        public string LocationName { get; set; }

        [Display(Name = "شناسه مکان والد")]
        public long? ParentLocationId { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        // Navigation Properties
        public Wh_Warehouse Warehouse { get; set; }
        public Wh_WarehouseLocation ParentLocation { get; set; }
        public ICollection<Wh_WarehouseLocation> SubLocations { get; set; }
    }

}
