namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class AccountsBrowserDto
    {
        public DocFilterDto? filter { get; set; }
        public List<Report_BrowserDto>? Kols { get; set; }
        public List<DocArticleDto>? Articles { get; set; }
        public ArticleAccountInfo navInfo { get; set; } = new ArticleAccountInfo();

    }
}
