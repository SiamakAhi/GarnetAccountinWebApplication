namespace GarnetAccounting.Areas.DataTransfer.Dto
{
    public class VmBillOfLandingMonitor
    {
        public DateTime? LastUpload { get; set; }
        public int sumOfBills { get; set; } = 0;
        public long amountOfBills { get; set; } = 0;
        public int sumOfNoFinal { get; set; } = 0;
        public long amountOfNoFinal { get; set; } = 0;
        public int sumOfNoDoc { get; set; } = 0;
        public long amountOfNoDoc { get; set; } = 0;
    }
}
