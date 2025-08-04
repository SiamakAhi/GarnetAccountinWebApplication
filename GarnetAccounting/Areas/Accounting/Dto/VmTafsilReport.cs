namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class VmTafsilReport
    {
        public TafsilReportFilterDto filter { get; set; }
        public List<TafsilReportDto> Report { get; set; }
        public List<CustomerCreditableTurnover> CreditableCustomerReport { get; set; }
        public string ReportTitle { get; set; }
        public string ReportSubtitle { get; set; }


    }
}
