using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Warehouse.Models.Entities
{
    public class Wh_Inventory
    {
        [Key]
        public Guid InventoryId { get; set; }

        [Display(Name = "شناسه فروشنده")]
        public long SellerId { get; set; }

        [Display(Name = "شناسه انبار")]
        public long WarehouseId { get; set; }

        [Display(Name = "شناسه محصول")]
        public long ProductId { get; set; }

        [Display(Name = "شناسه واحد کل")]
        public int PackageUnitId { get; set; } // واحد اندازه‌گیری کل

        [Display(Name = "شناسه واحد جزء")]
        public int BaseUnitId { get; set; } // واحد اندازه‌گیری جزء

        [Display(Name = "تعداد جزء در هر کل")]
        public int BasePerPackage { get; set; } // تعداد واحد جزء در هر واحد کل

        // **موجودی‌ها**
        [Display(Name = "موجودی کل - واحد کل")]
        public decimal TotalStockInPackage { get; set; }

        [Display(Name = "موجودی کل - واحد جزء")]
        public decimal TotalStockInBase { get; set; }

        [Display(Name = "موجودی قابل فروش - واحد کل")]
        public decimal AvailableStockInPackage { get; set; }

        [Display(Name = "موجودی قابل فروش - واحد جزء")]
        public decimal AvailableStockInBase { get; set; }

        [Display(Name = "موجودی رزرو شده - واحد کل")]
        public decimal ReservedStockInPackage { get; set; }

        [Display(Name = "موجودی رزرو شده - واحد جزء")]
        public decimal ReservedStockInBase { get; set; }

        [Display(Name = "موجودی امانی - واحد کل")]
        public decimal ConsignmentStockInPackage { get; set; }

        [Display(Name = "موجودی امانی - واحد جزء")]
        public decimal ConsignmentStockInBase { get; set; }

        [Display(Name = "موجودی درخواست شده - واحد کل")]
        public decimal RequestedStockInPackage { get; set; }

        [Display(Name = "موجودی درخواست شده - واحد جزء")]
        public decimal RequestedStockInBase { get; set; }

        [Display(Name = "موجودی در حال تأمین - واحد کل")]
        public decimal InSupplyStockInPackage { get; set; }

        [Display(Name = "موجودی در حال تأمین - واحد جزء")]
        public decimal InSupplyStockInBase { get; set; }

        [Display(Name = "موجودی ما نزد دیگران - واحد کل")]
        public decimal OurStockWithOthersInPackage { get; set; }

        [Display(Name = "موجودی ما نزد دیگران - واحد جزء")]
        public decimal OurStockWithOthersInBase { get; set; }

        [Display(Name = "آخرین بروزرسانی")]
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public virtual Wh_UnitOfMeasure BaseUnit { get; set; }
        public virtual Wh_UnitOfMeasure? PakageUnit { get; set; }
    }


}
