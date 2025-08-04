namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class CustomerCreditableTurnover
    {
        public long TafsilId { get; set; }
        public string TafsilName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? UntilDate { get; set; }
        public int BedQty { get; set; } = 0;
        public int BesQty { get; set; } = 0;
        public long Bed { get; set; } = 0;
        public long Bes { get; set; } = 0;
        public long Balance { get; set; } = 0;
        public Int16 BalanceNature { get; set; } = 0;

        public DateTime? LastBedDate { get; set; } = null;
        public DateTime? LastBesDate { get; set; } = null;

    }
}
