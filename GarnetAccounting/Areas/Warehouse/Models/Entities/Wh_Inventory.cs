using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarnetAccounting.Areas.Warehouse.Models.Entities
{
    public class Wh_Inventory
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int WarehouseId { get; set; }

        public int QuantityOnHand { get; set; } // موجودی فیزیکی
        public int QuantityAvailable { get; set; } // موجودی قابل فروش
        public int ReservedQuantity { get; set; } // موجودی رزرو شده

        [Column(TypeName = "decimal(18,2)")]
        public decimal AverageCost { get; set; } // میانگین قیمت تمام شده

        public DateTime LastUpdated { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual Wh_Product Product { get; set; }
        public virtual Wh_Warehouse Warehouse { get; set; }
    }
}
