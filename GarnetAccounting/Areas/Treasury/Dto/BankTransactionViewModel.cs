namespace GarnetAccounting.Areas.Treasury.Dto
{
    public class BankTransactionViewModel
    {
        public List<TreBankTransactionDto> transactions { get; set; } = new
            List<TreBankTransactionDto>();
        public BankReportFilterDto filter { get; set; } = new BankReportFilterDto();
    }
}
