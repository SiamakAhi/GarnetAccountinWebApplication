using GarnetAccounting.Areas.Commercial.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Warehouse.Models.Entities
{
    public class Wh_WarehouseDocumentItem
    {
        public Guid DocumentLineId { get; set; }

        [Display(Name = "شناسه سند انبار")]
        public Guid WarehouseDocumentId { get; set; }

        [Display(Name = "شناسه محصول")]
        public long ProductId { get; set; }

        [Display(Name = "شناسه واحد اندازه‌گیری")]
        public int UnitOfMeasureId { get; set; }

        [Display(Name = "تعداد در واحد انتخابی")]
        public decimal QuantityInUnit { get; set; }

        [Display(Name = "تعداد در واحد پایه")]
        public decimal QuantityInBaseUnit { get; set; }

        [Display(Name = "شناسه مکان انبار")]
        public long? LocationId { get; set; }

        [Display(Name = "شناسه خط فاکتور")]
        public Guid? InvoiceItemId { get; set; }

        [Display(Name = "توضیحات")]
        public string? Remarks { get; set; }


        // Navigation Properties
        public Wh_WarehouseDocument WarehouseDocument { get; set; }
        public Wh_Product Product { get; set; }
        public Wh_WarehouseLocation Location { get; set; }
        public com_InvoiceItem InvoiceItem { get; set; }
    }

}
