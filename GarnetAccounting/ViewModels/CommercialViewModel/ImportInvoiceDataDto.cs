namespace GarnetAccounting.ViewModels.CommercialViewModel
{
    public class ImportInvoiceDataDto
    {
        public string AccountingSystemCode { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int TypeId { get; set; }
        public string BuyerName { get; set; }
        public int BuyerTypeId { get; set; }
        public string BuyerNationalId { get; set; }
        public string BuyerEconomicCode { get; set; }
        public string BuyerPostalCode { get; set; }
        public string BuyerAddress { get; set; }
        public bool IsService { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int UnitCountCode { get; set; }
        public decimal Fee { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal AfterDiscount { get; set; }
        public decimal VatRate { get; set; }
        public decimal VarPrice { get; set; }
        public decimal FinalPrice { get; set; }


    }
}
