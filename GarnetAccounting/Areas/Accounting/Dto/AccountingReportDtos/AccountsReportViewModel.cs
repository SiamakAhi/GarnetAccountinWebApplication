namespace GarnetAccounting.Areas.Accounting.Dto.AccountingReportDtos
{
    public class AccountsReportViewModel
    {
        public DocFilterDto filter { get; set; } = new DocFilterDto();

        public List<AccountsReport> Reports { get; set; }
    }
}
