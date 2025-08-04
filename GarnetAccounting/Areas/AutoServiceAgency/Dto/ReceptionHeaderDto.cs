namespace GarnetAccounting.Areas.AutoServiceAgency.Dto
{
    public class ReceptionHeaderDto
    {
        public string ReceptionNumber { get; set; }
        public DateTime Date { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }


        public long SaleWarrantyAmount { get; set; } = 0;
        public long ojratWarrantyTotal { get; set; } = 0;

        public long SaleFreeAmount { get; set; } = 0;
        public long ojratFreeTotal { get; set; } = 0;

        public long VatWarranty => ((long)((ojratWarrantyTotal + SaleWarrantyAmount) * 10 / 100));
        public long VatFree => ((long)((ojratFreeTotal + SaleFreeAmount) * 10 / 100));

        public long WarrantyTotalPrice => ((long)(SaleWarrantyAmount + ojratWarrantyTotal + VatWarranty));
        public long FreeTotalPrice => ((long)(SaleFreeAmount + ojratFreeTotal + VatFree));

        public string CustomerName { get; set; }
        public string AgancyCode { get; set; }
        public string IncommType { get; set; }
        public string BachNumber { get; set; }
        public bool HasDoc { get; set; }
        public long? DocNum { get; set; }
        public string? ArchiveNumber { get; set; }
    }
}
