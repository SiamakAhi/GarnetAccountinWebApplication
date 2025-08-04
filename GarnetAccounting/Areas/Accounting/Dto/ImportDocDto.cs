namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class ImportDocDto
    {
        public int RowNumber { get; set; }
        public int DocNumber { get; set; }
        public string PersianDate { get; set; }
        public DateTime DocDate { get; set; }
        public string? Description { get; set; }
        public string? DocDescription { get; set; }
        public long Bed { get; set; }
        public long Bes { get; set; }
        public string? KolName { get; set; }
        public string? KolCode { get; set; }
        public string? MoeinName { get; set; }
        public string? MoeinCod { get; set; }
        public string? TafsilName { get; set; }
        public string? TafsilCode { get; set; }
        public string? TafsilName5 { get; set; }
        public string? TafsilCode5 { get; set; }
        public string? TafsilName6 { get; set; }
        public string? TafsilCode6 { get; set; }
        public string? TafsilName7 { get; set; }
        public string? TafsilCode7 { get; set; }
        public int OldDocnumber { get; set; }
    }
}
