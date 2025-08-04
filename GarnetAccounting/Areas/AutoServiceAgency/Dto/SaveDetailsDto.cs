namespace GarnetAccounting.Areas.AutoServiceAgency.Dto
{
    public class SaveDetailsDto
    {
        public string ReceptionNember { get; set; }
        public string ReceptionDate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Brand { get; set; }
        public DateTime Date { get; set; }
        public string ClientName { get; set; }
        public string CarNumber { get; set; }
        public string IncomeType { get; set; }
        public short IncomeTypeCode { get; set; }
        public long TotalSaleAmount { get; set; } = 0;
        public long TotalOjratAmount { get; set; } = 0;
        public long? BankTafsilId { get; set; }
        public bool CreateNewDoc { get; set; } = false;

        public long SaleWarrantyAmount { get; set; } = 0;
        public long ojratWarrantyTotal { get; set; } = 0;

        public long SaleFreeAmount { get; set; } = 0;
        public long ojratFreeTotal { get; set; } = 0;

        public long VatAmount { get; set; } = 0;
        public long VatWarranty => ((long)((ojratWarrantyTotal + SaleWarrantyAmount) * 10 / 100));
        public long VatFree => ((long)((ojratFreeTotal + SaleFreeAmount) * 10 / 100));

        public long WarrantyTotalPrice => ((long)(SaleWarrantyAmount + ojratWarrantyTotal + VatWarranty));
        public long FreeTotalPrice => ((long)(SaleFreeAmount + ojratFreeTotal + VatFree));


        public List<SaveDetails_ItemDto> Items { get; set; }
    }
}
