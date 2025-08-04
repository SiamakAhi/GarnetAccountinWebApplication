namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class VmProfit
    {
        public ProfitReportSetting? AccountsSetting { get; set; }
        public List<ProfitMasterDto>? ProfitReport { get; set; }
    }
}
