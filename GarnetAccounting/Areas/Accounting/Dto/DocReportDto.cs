namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class DocReportDto
    {
        public DocFilterDto? filter { get; set; } = new DocFilterDto();
        public List<DocArticleDto>? Articles { get; set; } = new List<DocArticleDto>();
        public Pagination<DocArticleDto>? ArticlesPagin { get; set; }
        public List<VmTurnoverAccount> TurnoverAccounts { get; set; } = new List<VmTurnoverAccount>();
    }
}
