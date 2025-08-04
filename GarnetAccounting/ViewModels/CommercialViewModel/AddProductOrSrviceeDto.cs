using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GarnetAccounting.ViewModels.CommercialViewModel
{
    public class AddProductOrSrviceeDto
    {
        [Display(Name = "شناسه کالا")]
        public Int64 Id { get; set; }

        [Display(Name = "گروه کالا یا خدمت")]
        public int? CategoryId { get; set; } = null;

        [Display(Name = "شناسه در سیستم حسابداری")]
        public string? AccountingSystemId { get; set; }

        [Display(Name = "شناسه 13 رقمی کالا *")]
        public string? UniqueId { get; set; } // 13 رقمی

        [Display(Name = "نام کالا *")]
        [Required(ErrorMessage = "نوشتن نام کالا الزامی است")]
        public string Name { get; set; }

        [Display(Name = "کد کالا")]
        public string? ProductCode { get; set; }

        [Display(Name = "قیمت (ریال)")]
        public decimal? Price { get; set; }

        [Display(Name = "نرخ ارزش افزوده *")]
        public float VATRate { get; set; } // نرخ ارزش افزوده

        [Display(Name = " واحد اندازه گیری *")]
        [Required(ErrorMessage = "واحد اندازه گیری را مشخص کنید")]
        public int UnitOfMeasurementId { get; set; }

        [AllowNull]
        public string? UnitOfMeasurementName { get; set; }
        //......

        [Display(Name = " نرخ تبدیل ارز *")]
        public decimal? ExchangeRate { get; set; }

        [Display(Name = "ارزش ارزی کالا *")]
        public decimal? ForeignCurrencyValue { get; set; }

        [Display(Name = "ارزش ریالی کالا *")]
        public decimal? LocalCurrencyValue { get; set; }

        [Display(Name = "نوع ارز *")]
        public int? CurrencyId { get; set; } = null;

        [Display(Name = "وزن خالص (کیلوگرم) *")]
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

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        [Display(Name = "کالا / خدمت *")]
        [Required(ErrorMessage = "کالا یا خدمت را از لیست انتخاب کنید")]
        public bool IsService { get; set; }

        public int customerId { get; set; }
        public Int64 SellerId { get; set; }
    }
}
