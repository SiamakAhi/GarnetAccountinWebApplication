using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Warehouse.Models.Entities
{
    public class Wh_WarehouseDocument
    {
        public Guid WarehouseDocumentId { get; set; }

        [Display(Name = "شناسه فروشنده")]
        public long SellerId { get; set; }

        [Display(Name = "نوع سند انبار")]
        public short DocumentType { get; set; }

        [Display(Name = "شماره سند")]
        public string DocumentNumber { get; set; }

        [Display(Name = "تاریخ سند")]
        public DateTime DocumentDate { get; set; }

        [Display(Name = "شناسه انبار مبدأ")]
        public long? SourceWarehouseId { get; set; }

        [Display(Name = "شناسه انبار مقصد")]
        public long? DestinationWarehouseId { get; set; }

        [Display(Name = "وضعیت سند")]
        public short DocumentStatus { get; set; }

        [Display(Name = "توضیحات")]
        public string? Remarks { get; set; }

        [Display(Name = "ایجاد کننده")]
        public string CreatorName { get; set; }
        public DateTime CreatDate { get; set; }
        public string? EditorName { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string? DeleteUserName { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }

        // Navigation Properties
        public Wh_Warehouse SourceWarehouse { get; set; }
        public Wh_Warehouse? DestinationWarehouse { get; set; }
        public ICollection<Wh_WarehouseDocumentItem> DocumentItems { get; set; }
    }
}
