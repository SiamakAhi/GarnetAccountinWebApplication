namespace GarnetAccounting.Areas.Accounting.Models.Entities
{
    public class Acc_CostCenter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public long? SellerId { get; set; }

        public long? TafsilId { get; set; }
        public string? TafsilCode { get; set; }
    }
}
