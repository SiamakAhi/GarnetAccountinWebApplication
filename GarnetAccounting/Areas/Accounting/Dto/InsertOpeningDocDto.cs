namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class InsertOpeningDocDto
    {
        public int PeriodId { get; set; }
        public int DestinationPeriodId { get; set; }
        public long SellerId { get; set; }
        public string CreatorUserName { get; set; }
    }
}
