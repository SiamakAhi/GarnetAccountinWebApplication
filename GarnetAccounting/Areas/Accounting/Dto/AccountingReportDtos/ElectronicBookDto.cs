namespace GarnetAccounting.Areas.Accounting.Dto.AccountingReportDtos
{
    public class ElectronicBookDto
    {
        public int Row { get; set; }
        public string docDate { get; set; }
        public string KolCode { get; set; }
        public string KolName { get; set; }
        public string? MoeinCode { get; set; }
        public string? MoeinName { get; set; }
        public string? Description { get; set; }
        public long Bed { get; set; } = 0;
        public long Bes { get; set; } = 0;
    }
}
