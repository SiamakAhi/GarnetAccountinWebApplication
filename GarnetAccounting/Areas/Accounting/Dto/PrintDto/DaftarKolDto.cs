namespace GarnetAccounting.Areas.Accounting.Dto.PrintDto
{
    public class DaftarKolDto
    {
        public string KolCode { get; set; }
        public string KolName { get; set; }
        public int DocNumber { get; set; }
        public string DocDate { get; set; }
        public string Atf { get; set; }
        public string Description { get; set; }
        public long Bed { get; set; }
        public long Bes { get; set; }
        public long Mandeh { get; set; }
        public string Tashkhis { get; set; }
        public int PageNumber { get; set; }
        public int RowNumber { get; set; }
        public int AccountPageNumber { get; set; }
    }
}
