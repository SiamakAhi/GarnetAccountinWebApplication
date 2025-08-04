using GarnetAccounting.Areas.Commercial.Dtos;
using GarnetAccounting.Areas.Treasury.Dto;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class VmDocCreator
    {
        public Pagination<InvoiceHeaderDto>? Invoices { get; set; }
        public Pagination<TreBankTransactionDto>? BankTransaction { get; set; }
        public DocCreatorFilterDto filter { get; set; }
    }
}
