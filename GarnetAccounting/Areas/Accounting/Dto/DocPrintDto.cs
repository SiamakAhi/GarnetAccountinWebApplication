namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class DocPrintDto
    {
        public DocHeaderPrintDto Header { get; set; }
        public List<DocArticlePrintDto> Articles { get; set; }
    }
}
