namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class ArticleAccountInfo
    {
        public int ReportLevel { get; set; }
        public int? KolId { get; set; }
        public int? MoeinId { get; set; }
        public long? Tafsil4Id { get; set; }
        public long? Tafsil5Id { get; set; }
        public long? Tafsil6Id { get; set; }
        public long? Tafsil7Id { get; set; }
        public long? Tafsil8Id { get; set; }

        public string? KolName { get; set; }
        public string? MoeinName { get; set; }
        public string? Tafsil4Name { get; set; }
        public string? Tafsil5Name { get; set; }
        public string? Tafsil6Name { get; set; }
        public string? Tafsil7Name { get; set; }
        public string? Tafsil8Name { get; set; }
    }
}
