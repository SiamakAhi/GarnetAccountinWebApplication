namespace GarnetAccounting.Areas.Accounting.Models.Entities
{
    public class Acc_ArchiveNumber
    {
        public int Id { get; set; }
        public long SellerId { get; set; }
        public int PeriodId { get; set; }
        public int DocAutonumber { get; set; }
        public int Counter { get; set; }
    }
}
