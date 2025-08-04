using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Warehouse.Models.Entities
{
    public class Wh_InventoryTransaction
    {
        public Guid TransactionId { get; set; }

        [Display(Name = "شناسه فروشنده")]
        public long SellerId { get; set; }

        [Display(Name = "نوع تراکنش")]
        public short TransactionType { get; set; }

        [Display(Name = "شناسه محصول")]
        public long ProductId { get; set; }

        [Display(Name = "شناسه انبار مبدأ")]
        public long SourceWarehouseId { get; set; }

        [Display(Name = "شناسه انبار مقصد")]
        public long? DestinationWarehouseId { get; set; }

        [Display(Name = "شناسه واحد اندازه‌گیری کل ")]
        public int UnitOfMeasureId { get; set; }

        [Display(Name = "تعداد در واحد کل")]
        public decimal QuantityInUnit { get; set; }

        [Display(Name = "تعداد کل")]
        public decimal PakageQuantity { get; set; }

        [Display(Name = "تعداد جزء")]
        public decimal BaseUnitQuantity { get; set; }

        [Display(Name = "تعداد به واحد اصلی")]
        public decimal TotalQuantity { get; set; }

        [Display(Name = "تاریخ تراکنش")]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "شناسه سند انبار")]
        public Guid WarehouseDocumentId { get; set; }

        [Display(Name = "توضیحات")]
        public string? Remarks { get; set; }

        // Navigation Properties
        public Wh_Product Product { get; set; }
        public Wh_WarehouseDocument WarehouseDocument { get; set; }
        public Wh_UnitOfMeasure UnitOfMeasure { get; set; }
        public Wh_Warehouse SourceWarehouse { get; set; }
        public Wh_Warehouse DestinationWarehouse { get; set; }
    }

}
