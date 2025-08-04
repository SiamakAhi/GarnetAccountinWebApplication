namespace GarnetAccounting.Areas.Accounting.Dto.PrintDto
{
    public class StructuredDocPrintDto
    {
        public DocHeaderPrintDto Header { get; set; }
        public List<StructuredKolGroupDto> KolGroups { get; set; }
    }

}
