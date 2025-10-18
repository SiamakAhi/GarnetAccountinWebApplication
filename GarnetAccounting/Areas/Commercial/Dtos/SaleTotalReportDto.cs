namespace GarnetAccounting.Areas.Commercial.Dtos
{
    public class SaleTotalReportDto
    {
        public int InvoiceQty { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public long PriceBeforDiscount { get; set; }
        public long Discount { get; set; }
        public long PriceAfterDiscount { get; set; }
        public long NoTaxable { get; set; }
        public long Taxable { get; set; }
        public long VarPrice { get; set; }
        public long TotalFinalPrice { get; set; }

        public InvoiceFilterDto filter { get; set; } = new InvoiceFilterDto();
    }
}
