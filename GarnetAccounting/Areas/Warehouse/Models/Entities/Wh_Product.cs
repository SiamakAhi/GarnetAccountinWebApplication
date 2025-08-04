using GarnetAccounting.Areas.Commercial.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Warehouse.Models.Entities
{
    public class Wh_Product
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

        [Display(Name = "شناسه واحد پایه")]
        public int BaseUnitId { get; set; }
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

        [Display(Name = "نرخ برابری ارز با ریال")]
        public decimal? ExchangeRate { get; set; }

        [Display(Name = "ارزش ارزی کالا")]
        public decimal? ForeignCurrencyValue { get; set; }

        [Display(Name = "ارزش ریالی کالا")]
        public decimal? LocalCurrencyValue { get; set; }

        [Display(Name = "شناسه ارز")]
        public int? CurrencyId { get; set; }

        [Display(Name = "وزن خالص (کیلوگرم)")]
        public float? NetWeight { get; set; }

        [Display(Name = "موضوع سایر وجوه قانونی")]
        public string? OtherLegalChargesSubject { get; set; }

        [Display(Name = "نرخ سایر وجوه قانونی")]
        public float? OtherLegalChargesRate { get; set; }

        [Display(Name = "مبلغ سایر وجوه قانونی")]
        public decimal? OtherLegalChargesAmount { get; set; }

        [Display(Name = "موضوع سایر مالیات و عوارض")]
        public string? OtherTaxesSubject { get; set; }

        [Display(Name = "نرخ سایر مالیات و عوارض")]
        public float? OtherTaxesRate { get; set; }

        [Display(Name = "مبلغ سایر مالیات و عوارض")]
        public decimal? OtherTaxesAmount { get; set; }

        [Display(Name = "دارای موجودی")]
        public bool HasInventory { get; set; }

        [Display(Name = "خدمت است")]
        public bool IsService { get; set; }

        [Display(Name = "حداقل موجودی مجاز")]
        public decimal? MinimumQuantity { get; set; }

        [Display(Name = "حداکثر موجودی مجاز")]
        public decimal? MaximumQuantity { get; set; }


        // Navigation Properties
        public virtual Wh_ProductCategory ProductCategory { get; set; }
        public virtual Wh_UnitOfMeasure BaseUnit { get; set; }
        public virtual Wh_UnitOfMeasure? PakageUnit { get; set; }
        public virtual ICollection<Wh_ProductUnit> ProductUnits { get; set; } = new List<Wh_ProductUnit>();
        public virtual ICollection<com_InvoiceItem> InvoiceItems { get; set; } = new List<com_InvoiceItem>();
        public virtual ICollection<Wh_WarehouseDocumentItem> WarehouseDocumentItems { get; set; } = new List<Wh_WarehouseDocumentItem>();
    }

}
