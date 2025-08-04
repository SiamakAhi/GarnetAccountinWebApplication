using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Commercial.Dtos
{
    public class InvoiceFilterDto
    {
        public long SellerId { get; set; }
        public int? PeriodId { get; set; }
        public short Invoicetype { get; set; }

        [Display(Name = "شماره فاکتور")]
        public string? InvoiceNumber { get; set; }

        [Display(Name = "از تاریخ")]
        public string? srtFromDate { get; set; }

        [Display(Name = "تا تاریخ")]
        public string? srtToDate { get; set; }

        [Display(Name = "طرف حساب")]
        public long? Party { get; set; }

        [Display(Name = "توضیحات")]
        public string? Remark { get; set; }

        public bool Taged { get; set; }

        [Display(Name = "وضعیت")]
        public string? Status { get; set; }
        public bool? WithVat { get; set; }

        [Display(Name = "صفحه")]
        public int CurrentPage { get; set; } = 1;

        [Display(Name = "تعداد ردیف در هر صفحه")]
        public int PageSize { get; set; } = 25;
        public Guid? DocId { get; set; }
        public int? DocNo { get; set; }

    }
}
