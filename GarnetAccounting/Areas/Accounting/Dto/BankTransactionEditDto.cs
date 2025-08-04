namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class BankTransactionEditDto
    {

        public long Id { get; set; }
        public string? Comment { get; set; }
        public string? TransactionNumber { get; set; }
        public string? TransactionDate { get; set; }
        public string? Description { get; set; }
    }
}
