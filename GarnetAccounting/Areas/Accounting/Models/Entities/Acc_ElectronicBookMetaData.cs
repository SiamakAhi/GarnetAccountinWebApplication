namespace GarnetAccounting.Areas.Accounting.Models.Entities
{
    public class Acc_ElectronicBookMetaData
    {
        public int Id { get; set; }
        public long SellerId { get; set; }
        public int PeriodId { get; set; }
        public bool IsSent { get; set; } = false;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int MinDocNumber { get; set; }
        public int MaxDocNumber { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public string? IsDeletedBy { get; set; }
        public DateTime? IsDeletedAt { get; set; }
    }
}
