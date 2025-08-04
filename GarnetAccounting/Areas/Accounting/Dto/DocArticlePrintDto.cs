namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class DocArticlePrintDto
    {
        public Guid Id { get; set; }
        public Guid DocId { get; set; }
        public int? KolId { get; set; }
        public int MoeinId { get; set; }
        public string KolName { get; set; }
        public string KolCode { get; set; }
        public string MoeinCode { get; set; }
        public string MoeinName { get; set; }
        public string Tafsil4 { get; set; }
        public string Tafsil5 { get; set; }
        public long Amount { get; set; }
        public long Bed { get; set; }
        public long Bes { get; set; }
        public string strAmount { get; set; }
        public string? Comment { get; set; }
        public int Nature { get; set; }
    }
}
