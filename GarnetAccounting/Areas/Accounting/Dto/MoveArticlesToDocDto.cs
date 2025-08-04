namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class MoveArticlesToDocDto
    {
        public List<Guid> ArticlesId { get; set; }
        public List<DocArticleDto>? Articles { get; set; }
        public Guid DocId { get; set; }
        public Guid CurrentDocId { get; set; }
        public string userName { get; set; }
    }
}
