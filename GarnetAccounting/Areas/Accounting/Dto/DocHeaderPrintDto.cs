namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class DocHeaderPrintDto
    {
        public Guid Id { get; set; }
        public string FinancePeriodName { get; set; }
        public DateTime? DocDate { get; set; }
        public string DocDate_Sh { get; set; }
        public int DocNumber { get; set; }
        public int DocAtf { get; set; }
        public int DocAutoNumber { get; set; }
        public string? Description { get; set; }
        public string? strTotal { get; set; }
        public string? Auther { get; set; }

    }
}
