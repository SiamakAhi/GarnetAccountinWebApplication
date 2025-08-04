namespace GarnetAccounting.ViewModels.CommercialViewModel
{
    public class VmSellerDashboard
    {
        public decimal? TotatSaleAmount { get; set; }
        public decimal? TotamVat { get; set; }
        public decimal? WaitingVat { get; set; }
        public int SaleInvoiceCount { get; set; }
        public int AcceptedInvoiceCount { get; set; }
        public decimal? AcceptedInvoiceAmount { get; set; }
        public int RejectedInvoiceCount { get; set; }
        public decimal? RejectedInvoiceAmount { get; set; }
        public int NotSendInvoiceCount { get; set; }
        public decimal? NotSendInvoiceAmount { get; set; }
        public int WaitingInvoiceCount { get; set; }
        public decimal? WaitingInvoiceAmount { get; set; }
        public int SellersCustomerCount { get; set; }
    }
}
