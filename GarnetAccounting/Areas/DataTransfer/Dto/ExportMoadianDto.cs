namespace GarnetAccounting.Areas.DataTransfer.Dto
{
    public class ExportMoadianDto
    {
        public string AccountingSystemInvoiceCode { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public int InvoiceType { get; set; }
        public string BuyerFullName { get; set; }
        public string PersonType { get; set; }
        public string NationalID { get; set; }
        public string NewEconomicCode { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string GoodsOrServices { get; set; }
        public string GoodsOrServices13DigitID { get; set; }
        public string GoodsOrServicesDescription { get; set; }
        public string GoodsOrServicesUnitCode { get; set; }
        public long UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public long Discount { get; set; }
        public long VATRate { get; set; }
        public long VATAmount { get; set; }
        public int SettlementType { get; set; } = 1;
    }
}

