namespace GarnetAccounting.Areas.AutoServiceAgency.Dto
{
    public class ImpodtReceptionDto
    {
        public IFormFile ExcelFile { get; set; }
        public long SellerId { get; set; }
        public string UserCreator { get; set; }
        public string Brand { get; set; }
        public int? PeriodId { get; set; }
    }
}
