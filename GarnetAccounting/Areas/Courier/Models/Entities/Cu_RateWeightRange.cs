namespace GarnetAccounting.Areas.Courier.Models.Entities
{
    // کلاس رنج وزنی
    public class Cu_RateWeightRange
    {
        public int Id { get; set; }
        public double StartWeight { get; set; }
        public double EndWeight { get; set; }

        // درصد از نرخ پایه
        public decimal Courier_WeightFactorPercent { get; set; }
        public decimal IATA_WeightFactorPercent { get; set; }
    }
}
