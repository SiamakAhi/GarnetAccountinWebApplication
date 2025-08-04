namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class TafsilReportDto
    {
        public Guid ArtId { get; set; }
        public Guid DocId { get; set; }
        public int DocNumber { get; set; }
        public int RowNumber { get; set; }
        public DateTime DocDate { get; set; }
        public string strDocDate { get; set; }

        public string KolCode { get; set; }
        public string KolName { get; set; }
        public int MoeinId { get; set; }
        public string MoeinCode { get; set; }
        public string MoeinName { get; set; }
        public string? Comment { get; set; }

        public long? Tafsil4Id { get; set; }
        public long? Tafsil5Id { get; set; }
        public long? Tafsil6Id { get; set; }
        public long? Tafsil7Id { get; set; }
        public long? Tafsil8Id { get; set; }

        public string? Tafsil4Name { get; set; }
        public string? Tafsil5Name { get; set; }
        public string? Tafsil6Name { get; set; }
        public string? Tafsil7Name { get; set; }
        public string? Tafsil8Name { get; set; }
        public long Bed { get; set; }
        public long Bes { get; set; }
        public long Balace { get; set; }
        public short BalaceNature { get; set; }
        public string? BalaceNatureName { get; set; }
        public string? cssClassName { get; set; }
    }
}
