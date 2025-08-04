using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Commercial.Dtos
{
    public class InvoiceHeaderDto
    {
        [Key]
        public Guid Id { get; set; }
        public long? SellerId { get; set; }
        public int? FinancePeriodId { get; set; }
        public short InvoiceType { get; set; }
        public int? InvoiceSubject { get; set; }

        [Display(Name = "شماره فاکتور")]
        [Required(ErrorMessage = "شماره فاکتور را وارد نمایید")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "شماره مالیاتی فاکتور")]
        public string? TaxInvoiceNumber { get; set; }

        [Display(Name = "شماره سیستمی فاکتور")]
        public long? InvoiceAutoNumber { get; set; }
        public int? SequenceNumber { get; set; }

        [Display(Name = "شناسه بایگانی")]
        public string? ArchiveRef { get; set; }

        [Display(Name = "تاریخ فاکتور")]
        public DateTime? InvoiceDate { get; set; }
        [Display(Name = "تاریخ فاکتور")]
        [Required(ErrorMessage = "شماره فاکتور را وارد نمایید")]
        public string strInvoiceDate { get; set; }

        [Display(Name = "طرف حساب")]
        [Required(ErrorMessage = "انتخاب خریدار الزامی است")]
        public long PartyId { get; set; }
        public long? PartyTafsilId { get; set; }
        public string? PartyName { get; set; }

        [Display(Name = "ویزیتور")]
        public long? Visitor { get; set; }
        [Display(Name = "درصد ویزیتور")]
        public short? VisitorPercent { get; set; }

        [Display(Name = "نوع تسویه")]
        public int? SettlementTypeId { get; set; }
        public string? SettlementTypeName { get; set; }


        public decimal? TotalPriceBeforDiscount { get; set; }
        public decimal? TotalTaxable { get; set; }
        public decimal? TotalNoTaxable { get; set; }
        public decimal? TotalDiscount { get; set; }
        public decimal? TotalPriceAfterDiscount { get; set; }
        public decimal? TotalVatPrice { get; set; }
        public decimal? TotalFinalPrice { get; set; }

        [Display(Name = "توضیحات")]
        public string? Remarks { get; set; }

        //1-یادداشت
        //2-جدید
        //3-در انتظار تسویه حساب
        //4- صدور حواله انبار
        //5-ابطال شده
        [Display(Name = "وضعیت")]
        public short? status { get; set; }

        [Display(Name = "زمان ایجاد")]
        public DateTime? CreationTime { get; set; }

        [Display(Name = " کاربر ایجاد کننده")]
        public string? CreatorUserId { get; set; }

        [Display(Name = "آخرین ویرایش")]
        public DateTime? LastUpdate { get; set; }

        [Display(Name = " کاربر ویرایش کننده")]
        public string? EditorUserId { get; set; }

        [Display(Name = "تعداد فاکتور")]
        public int InviceQty { get; set; }
        public bool taged { get; set; }
        public Guid? DocId { get; set; }
        public int? DocAutoNo { get; set; }

    }
}
