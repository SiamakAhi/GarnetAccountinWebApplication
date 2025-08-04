namespace GarnetAccounting.Areas.Accounting.Dto.PrintDto
{
    public class StructuredTafsilDto
    {
        public int KolId { get; set; }
        public int MoeinId { get; set; }
        public long? Tafsil4Id { get; set; }
        public string Tafsil4Name { get; set; }
        public long Bed { get; set; }
        public long Bes { get; set; }
        public string Comment { get; set; }
    }

}
