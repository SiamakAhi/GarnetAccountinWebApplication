using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Warehouse.Models.Dtos
{
    public class ProductDto
    {
        public long ProductId { get; set; }

        [Display(Name = "شناسه فروشنده")]
        public long SellerId { get; set; }

        [Display(Name = "کد محصول")]
        public string ProductCode { get; set; }

        [Display(Name = "شناسه یکتا")]
        public string? UniqueId { get; set; } // 13 رقمی

        [Display(Name = "نام محصول")]
        public string ProductName { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        [Display(Name = "شناسه دسته‌بندی")]
        public long? CategoryId { get; set; }

        [Display(Name = "نام گروه")]
        public string? CategoryName { get; set; }

        [Display(Name = "شناسه واحد پایه")]
        public int BaseUnitId { get; set; }
        [Display(Name = "نام واحد پایه")]
        public string? BaseUnitName { get; set; }
        public int? PakageCountId { get; set; }
        [Display(Name = "نام واحد بسته بندی")]
        public string? PakageUnitName { get; set; }

        // ارتباط با کلاس SaleUnitCount
        [Display(Name = "تعداد واحد در هر بسته")]
        public int QuantityInPakage { get; set; }

        [Display(Name = "نوع محصول")]
        public Int16 ProductType { get; set; }

        [Display(Name = "قیمت (ریال)")]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "نرخ ارزش افزوده")]
        public float VATRate { get; set; } // نرخ ارزش افزوده

        [Display(Name = "فعال")]
        public bool IsActive { get; set; }


    }
}
