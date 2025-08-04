namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class AccountBalanceDto
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountCode { get; set; }
        public long TotalBed { get; set; }
        public long TotalBes { get; set; }
        public long TotalBalance { get; set; }
        public Int16 AccountNature { get; set; }
        public int BalanceNature { get; set; }
    }
}
