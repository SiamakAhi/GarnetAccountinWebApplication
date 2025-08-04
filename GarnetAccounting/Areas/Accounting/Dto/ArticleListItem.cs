namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class ArticleListItem
    {
        public int Count { get; set; }
        public string ArchveCode { get; set; }
        public long TotalBed { get; set; }
        public long TotalBes { get; set; }
        public long Balance { get; set; }
        public string Nature { get; set; }
    }
}
