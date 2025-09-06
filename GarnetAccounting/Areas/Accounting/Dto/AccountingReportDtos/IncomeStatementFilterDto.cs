namespace GarnetAccounting.Areas.Accounting.Dto.AccountingReportDtos
{
    public class IncomeStatementFilterDto
    {
        public long SellerId { get; set; }
        public int PeriodId { get; set; }
        public string strStartDate { get; set; }
        public string strEndDate { get; set; }
        public string? Message { get; set; }
        public AccSettingDto AccountsInfo { get; set; }
        public List<ProfitMasterDto>? ProfitReport { get; set; } = new List<ProfitMasterDto>();

    }
}
