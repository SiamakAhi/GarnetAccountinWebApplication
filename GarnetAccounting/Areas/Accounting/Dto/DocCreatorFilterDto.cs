using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class DocCreatorFilterDto
    {
        public long SellerId { get; set; }
        public int? PeriodId { get; set; }

        public short? DocType { get; set; }

        [Display(Name = "شماره فاکتور")]
        public string? InvoiceNumber { get; set; }

        [Display(Name = "از تاریخ")]
        public string? srtFromDate { get; set; }

        [Display(Name = "تا تاریخ")]
        public string? srtToDate { get; set; }

        public bool Taged { get; set; }

        [Display(Name = "صفحه")]
        public int CurrentPage { get; set; } = 1;

        [Display(Name = "تعداد ردیف در هر صفحه")]
        public int PageSize { get; set; } = 50;

    }
}
