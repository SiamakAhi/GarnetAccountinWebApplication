namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class DocsMergeDto
    {
        public DocMerge_Header Doc { get; set; }
        public List<DocMerge_Article>? Articles { get; set; }
    }
}
