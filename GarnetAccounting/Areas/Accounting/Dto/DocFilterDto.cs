
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class DocFilterDto
    {
        [Display(Name = "شرکت")]
        public long SellerId { get; set; }

        [Display(Name = "دوره مالی")]
        public int PeriodId { get; set; }

        [Display(Name = "از تاریخ")]
        public DateTime? StartDate { get; set; }
        public string? strStartDate { get; set; }

        [Display(Name = "تا تاریخ")]
        public DateTime? EndDate { get; set; }
        public string? strEndDate { get; set; }

        [Display(Name = "از شماره")]
        public int? FromDocNumer { get; set; }

        [Display(Name = "تا شماره ")]
        public int? ToDocNumer { get; set; }

        [Display(Name = "شرح سند")]
        public string? Description { get; set; }

        [Display(Name = "صادر کننده")]
        public string? Subsystem { get; set; }

        [Display(Name = "نوع سند ")]
        public short? docType { get; set; }
        public int? KolId { get; set; }
        public int? MoeinId { get; set; }

        public int? targetId { get; set; }
        public long? tafsilId { get; set; }
        public long? tafsil4Id { get; set; }

        public long? tafsil5Id { get; set; }
        public long? tafsil6Id { get; set; }
        public long? tafsil7Id { get; set; }
        public long? tafsil8Id { get; set; }
        public long? CurrentTafsilId { get; set; }

        public int? ReportLevel { get; set; }
        public int? BalanceColumnsQty { get; set; }
        public List<int>? KolsId { get; set; }
        public List<int>? MoeinIds { get; set; }
        public List<long> TafilIds { get; set; }
        public List<long?> Tafsil4Ids { get; set; }
        public List<long?> Tafsil5Ids { get; set; }
        public List<long?> Tafsil6Ids { get; set; }
        public List<long?> Tafsil7Ids { get; set; }
        public List<long?> Tafsil8Ids { get; set; }
        public long Amount { get; set; }
        public string strAmount { get; set; }
        public int SearchAmountFild { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 25;
        public int? ProjectId { get; set; }
        public bool LevelUp { get; set; } = false;

        public bool FromBody { get; set; }

    }
}
