namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class TurnoverAccount_Items
    {
        public int DocNumber { get; set; }
        public DateTime DocDate { get; set; }
        public string Description { get; set; }
        public long Bed { get; set; }
        public long Bes { get; set; }
        public string Status { get; set; }
        public long Balance { get; set; }
    }
}
