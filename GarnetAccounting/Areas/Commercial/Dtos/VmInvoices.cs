namespace GarnetAccounting.Areas.Commercial.Dtos
{
    public class VmInvoices
    {
        public Pagination<InvoiceHeaderDto> Invoices { get; set; }
        public InvoiceFilterDto filter { get; set; }
    }
}
