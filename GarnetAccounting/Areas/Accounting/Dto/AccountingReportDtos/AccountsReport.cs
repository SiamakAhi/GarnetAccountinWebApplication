namespace GarnetAccounting.Areas.Accounting.Dto.AccountingReportDtos
{
    public class AccountsReport
    {
        public int? AccountId { get; set; }
        public short ReportLevel { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public long Bed { get; set; } = 0;
        public long Bes { get; set; } = 0;
        public long BalanceBed { get; set; } = 0;
        public long BalanceBes { get; set; } = 0;
        public List<MoeinReport> Moeins { get; set; }

    }
}
