namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class TarazAzmayeshiDto
    {

        public string? AccountKolName { get; set; }
        public string? AccountKolCode { get; set; }
        public string? AccountMoeinName { get; set; }
        public string? AccountMoeinCode { get; set; }
        public string? AccountTafsil4Name { get; set; }
        public string? AccountTafsil4Code { get; set; }


        public Int16 AccountNature { get; set; }
        public long TotalBed { get; set; }
        public long TotalBes { get; set; }
        public long MandehBed { get; set; }
        public long MandehBes { get; set; }
        public long AvalDoreh { get; set; }
        public long PayanDore { get; set; }
        public long StartBed { get; set; }
        public long StartBes { get; set; }
        public bool warning { get; set; }
        public int? BalanceLevel { get; set; }
        public Int16 BalanceNature { get; set; }
        public bool IsMatch { get; set; }
    }
}
