namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class VmTurnoverAccount
    {

        public string AccountCode { get; set; }
        public string KolName { get; set; }
        public string MoeinName { get; set; }
        public string TafsilName { get; set; }
        public long TotalBed { get; set; } = 0;
        public long TotalBes { get; set; } = 0;
        public long Balance { get; set; } = 0;
        public List<TurnoverAccount_Items> Items { get; set; } = new();
    }
}
