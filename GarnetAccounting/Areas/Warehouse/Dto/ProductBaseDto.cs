using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Warehouse.Dto
{
    public class ProductBaseDto
    {

        public long Id { get; set; }

        [Display(Name = "شناسه فروشنده")]
        public long SellerId { get; set; }

        [Display(Name = "کد محصول")]
        public string ProductCode { get; set; }

        [Display(Name = "شناسه یکتا")]
        public string? UniqueId { get; set; }

        [Display(Name = "نام محصول")]
        public string ProductName { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        [Display(Name = "شناسه دسته‌بندی")]
        public long? CategoryId { get; set; }
        public string? CategoryName { get; set; }

        [Display(Name = "شناسه واحد پایه")]
        public int BaseUnitId { get; set; }
        public string? BaseUnitName { get; set; }
        public string? PakageUnitName { get; set; }
        public int? PakageCountId { get; set; }
        // ارتباط با کلاس SaleUnitCount
        [Display(Name = "تعداد واحد در هر بسته")]
        public int QuantityInPakage { get; set; }

        [Display(Name = "نوع محصول")]
        public Int16? ProductType { get; set; }

        [Display(Name = "قیمت (ریال)")]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "نرخ ارزش افزوده")]
        public float VATRate { get; set; } // نرخ ارزش افزوده

        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        [Display(Name = "دارای موجودی")]
        public bool HasInventory { get; set; }

        [Display(Name = "خدمت است")]
        public bool IsService { get; set; }
        public bool KeepFormAfterSave { get; set; }
        public bool KeepDataAfterSave { get; set; }
    }
}
