namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class TrialBalancePrintDto
    {
        public string AccountName { get; set; }
        public string KolName { get; set; }
        public string MoeinName { get; set; }
        public string TafsilName { get; set; }
        public string AccountNature { get; set; }
        public long TotalBed { get; set; }
        public long TotalBes { get; set; }
        public long MandehBed { get; set; }
        public long MandehBes { get; set; }
        public long StartBed { get; set; }
        public long StartBes { get; set; }

    }
}
