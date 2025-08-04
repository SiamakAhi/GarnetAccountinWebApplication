namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class ConvertAccountsDto
    {
        public List<Guid> ArticleIds { get; set; }
        public int? NewMoeinId { get; set; }
        public short Tafsil4Action { get; set; } = 1;
        public long? Tafsil4Id { get; set; }
        public short Tafsil5Action { get; set; } = 1;
        public long? Tafsil5Id { get; set; }
        public short Tafsil6Action { get; set; } = 1;
        public long? Tafsil6Id { get; set; }
    }
}
