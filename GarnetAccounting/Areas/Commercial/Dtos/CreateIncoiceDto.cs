namespace GarnetAccounting.Areas.Commercial.Dtos
{
    public class CreateIncoiceDto
    {
        public InvoiceHeaderDto Header { get; set; }
        public List<InvoiceItemDto> Item { get; set; } = new List<InvoiceItemDto>();
    }
}
