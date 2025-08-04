namespace GarnetAccounting.Areas.Courier.Dto.RepresentativeDtos
{
    public class RepresentativeRate
    {
        public string Name { get; set; }
        public string City { get; set; }
        public DateTime? LastDayOfWork { get; set; }
        public int Qty { get; set; }
        public long TotalBill { get; set; }
        public long RepresentativeShare { get; set; }
        public long? CashOnDelivery { get; set; }
        public long? PaidAmounts { get; set; }
        public long? TafsilId { get; set; }
    }
}
