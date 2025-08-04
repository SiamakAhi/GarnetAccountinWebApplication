namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class TafsilReportFilterDto
    {
        public long SellerId { get; set; }
        public int PeriodId { get; set; }
        public int? TafsilGroup { get; set; }
        public List<long> Tafsil4Ids { get; set; }
        public List<long> Tafsil5Ids { get; set; }
        public List<int>? Kols { get; set; }
        public List<int>? Moeins { get; set; }
        public string? strStartDate { get; set; }
        public string? strEndDate { get; set; }
        public string? SelectedTafsilTexts { get; set; }
        public int? ProjectId { get; set; }
        public bool ShowJustWithValue { get; set; } = false;
        public short ReportLevel { get; set; } = 4;
    }
}
