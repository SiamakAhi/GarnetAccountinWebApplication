namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class BalanceDataDto
    {

        public short GroupType { get; set; }
        public string GroupTypeName { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string KolName { get; set; }
        public string KolCode { get; set; }
        public int KolId { get; set; }
        public long Bed { get; set; }
        public long Bes { get; set; }
        public long Mandeh { get; set; }
    }
}
