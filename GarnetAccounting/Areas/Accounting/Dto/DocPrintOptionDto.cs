namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class DocPrintOptionDto
    {
        public Guid? DocId { get; set; }
        public List<Guid>? DocIds { get; set; }
        public int PrintLevel { get; set; }
    }
}
