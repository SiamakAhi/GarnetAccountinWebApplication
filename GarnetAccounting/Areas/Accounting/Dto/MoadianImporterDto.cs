using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class MoadianImporterDto
    {
        public long SellerId { get; set; }

        [Display(Name = "نوع گزارش ")]
        public short ReportType { get; set; }
        public int PeriodId { get; set; }

        [Display(Name = "انتخاب اکسل گزارش")]
        public IFormFile File { get; set; }
    }
}
