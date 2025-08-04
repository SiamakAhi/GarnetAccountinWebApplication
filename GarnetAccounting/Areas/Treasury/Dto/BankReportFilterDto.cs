namespace GarnetAccounting.Areas.Treasury.Dto
{
    public class BankReportFilterDto
    {
        public long SellerId { get; set; }
        public long AccountId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string strFromDate { get; set; }
        public string strToDate { get; set; }
        public string Description { get; set; }
        public int Transactiontype { get; set; } = 0;
        public bool? HasDoc { get; set; } = false;
        public bool? IsChecked { get; set; } = false;
        public bool ShowAll { get; set; } = false;
        public short searchtype { get; set; } = 1;
        public List<long> TransactionsId { get; set; }
    }
}
