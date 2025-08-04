namespace GarnetAccounting.Areas.Accounting.Dto.Moadian
{
    public class MoadianFilterDto
    {
        public long SellerId { get; set; }
        public int PeriodId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
