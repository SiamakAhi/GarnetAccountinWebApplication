namespace GarnetAccounting.Areas.Accounting.Dto.AccountingReportDtos
{
    public class ElectronicBookVm
    {
        public DocFilterDto filter { get; set; } = new DocFilterDto();
        public List<ElectronicBookDto> Articles { get; set; } = new List<ElectronicBookDto>();
        public string? Message { get; set; }
    }
}
