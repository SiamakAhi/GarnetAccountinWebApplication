namespace GarnetAccounting.Areas.Accounting.Dto.PrintDto
{
    public class TafsilTurnoverPrintDto
    {
        public int DocNumber { get; set; }
        public string DocDate { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string TafsilName { get; set; }
        public string Description { get; set; }
        public long Bed { get; set; }
        public long Bes { get; set; }
        public long Mandeh { get; set; }
        public string AccountStatus { get; set; }
    }
}
