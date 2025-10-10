using GarnetAccounting.Areas.Accounting.Dto.AccountingReportDtos;
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class EBookManagerDto
    {
        public int Id { get; set; }
        public long SellerId { get; set; }
        public int PeriodId { get; set; }

        [Display(Name = "وضعیت ارسال به سامانه")]
        public bool IsSent { get; set; } = false;

        [Display(Name = "براساس تاریخ")]
        public bool ByDate { get; set; } = false;

        [Display(Name = "از تاریخ")]
        [Required(ErrorMessage = "تاریخ شروع را مشخص کنید")]
        public DateTime FromDate { get; set; }

        [Display(Name = "تا تاریخ")]
        [Required(ErrorMessage = "تاریخ آخرین سند را مشخص کنید")]
        public DateTime ToDate { get; set; }

        [Display(Name = "از سند")]
        [Required(ErrorMessage = "شماره سند شروع را مشخص کنید")]
        public int MinDocNumber { get; set; }

        [Display(Name = "تا سند")]
        [Required(ErrorMessage = "شماره آخرین سند را مشخص کنید")]
        public int MaxDocNumber { get; set; }

        [Display(Name = "ایجاد شده توسط")]
        public string CreateBy { get; set; }

        [Display(Name = "زمان ایجاد")]
        public DateTime CreateAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;

        public string? IsDeletedBy { get; set; }

        public DateTime? IsDeletedAt { get; set; }

        public List<ElectronicBookDto>? eBooks { get; set; }
        public List<EBookManagerDto>? eBooksMetaData { get; set; }

        //
        public string? Message { get; set; }
        public bool Successed { get; set; } = false;
    }
}
