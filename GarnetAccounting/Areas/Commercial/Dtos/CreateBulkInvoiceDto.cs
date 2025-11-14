namespace GarnetAccounting.Areas.Commercial.Dtos
{
    public class CreateBulkInvoiceDto
    {
        //1- خرید
        //2- فروش
        public short InvoiceType { get; set; }
        public bool JustTaxable { get; set; } = false;
        public short template { get; set; }
        public IFormFile ExcelFile { get; set; }

    }
}
