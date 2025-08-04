namespace GarnetAccounting.Areas.Courier.Models.Entities
{
    public class Cu_RateBaseKValue
    {
        public short Id { get; set; }
        public long SellerId { get; set; }

        // مقدار پایه نرخ 
        public long KValue { get; set; }
    }
}
