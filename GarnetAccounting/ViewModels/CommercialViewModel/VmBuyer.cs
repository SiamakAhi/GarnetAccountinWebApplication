namespace GarnetAccounting.ViewModels.CommercialViewModel
{
    public class VmBuyer
    {
        public Int64 Id { get; set; }
        public int customerId { get; set; }
        public short legalStatus { get; set; }
        public string Name { get; set; }
        public string NotionalIdentity { get; set; }
        public string EconimicCode { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string phone { get; set; }
    }
}
