namespace GarnetAccounting.Areas.Commercial.Dtos
{
    public class ImportRawData_Atiran
    {
        public string InvoiceNumer { get; set; }
        public string? CategoryName { get; set; }
        public string strDate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string? personCode { get; set; }
        public string personName { get; set; }
        public string? personNationalId { get; set; }
        public string? personEcconomicCode { get; set; }
        public string? ProductCode { get; set; }
        public string ProductName { get; set; }
        public string? PakageUnitCountName { get; set; }
        public string? baseUnitCountName { get; set; }
        public int QtyBaseUnitInPakage { get; set; }
        public decimal QtyPakage { get; set; }
        public decimal QtyBase { get; set; }
        public decimal Fee { get; set; }
        public decimal PriceBeforeDiscount { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public decimal Discount { get; set; }
        public decimal VatRate { get; set; }
        public decimal VatPrice { get; set; }
        public decimal FinalPrice { get; set; }


    }
}
