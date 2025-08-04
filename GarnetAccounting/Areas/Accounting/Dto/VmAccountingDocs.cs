namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class VmAccountingDocs
    {
        public List<DocDto> Docs { get; set; }
        public DocFilterDto filter { get; set; }
        public Pagination<DocDto> DocsPagin { get; set; }
    }
}
