namespace GarnetAccounting.Areas.Treasury.Dto
{
    public class kh_BankAccountDto
    {
        public int Id { get; set; }
        public long SellerId { get; set; }
        public int? BankId { get; set; }
        public string? BranchCode { get; set; }
        public string? AccountName { get; set; }
        public string? AccountType { get; set; }
        public string? AccountNumber { get; set; }
        public string? SHABA { get; set; }
        public string? CardNumber { get; set; }
        public string? cvvt { get; set; }
        public string? CardDate { get; set; }
        public string? BankAddress { get; set; }
        public long? TafsilId { get; set; }
        public string? TafsilCode { get; set; }

        public bool IsActive { get; set; }
    }
}
