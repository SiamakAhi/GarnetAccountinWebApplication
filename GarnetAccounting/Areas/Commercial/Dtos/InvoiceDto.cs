using GarnetAccounting.ViewModels;

namespace GarnetAccounting.Areas.Commercial.Dtos
{
    public class InvoiceDto
    {
        public InvoiceHeaderDto? InvoiceHeader { get; set; }
        public ICollection<InvoiceItemDto> Items { get; set; } = new List<InvoiceItemDto>();
        public InvoiceItemDto? dto { get; set; }
        public PersonDto? buyerInfo { get; set; }
        public PersonDto? sellerInfo { get; set; }
    }
}
