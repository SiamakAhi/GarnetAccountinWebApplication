namespace GarnetAccounting.Areas.DataTransfer.Dto
{
    public class ImportSaleDocDto
    {
        public int RowNumber { get; set; }
        public DateTime DocDate { get; set; }
        public string? Description { get; set; }
        public long Bed { get; set; }
        public long Bes { get; set; }
        public string? KolCode { get; set; }
        public string? MoeinCod { get; set; }
        public string? Tafsil4Name { get; set; }
        public string? Tafsil5Name { get; set; }
        public string? Tafsil6Name { get; set; }
        public string? Tafsil7Name { get; set; }
        public string? BillOfLadingNumber { get; set; }
        public long BillId { get; set; }
    }
}
