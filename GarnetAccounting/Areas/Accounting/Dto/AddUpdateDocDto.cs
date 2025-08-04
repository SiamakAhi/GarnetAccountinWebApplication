namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class AddUpdateDocDto
    {
        public DocDto? Doc { get; set; }
        public DocArticleDto? Article { get; set; }
        public MoeinStatusDto? AccountStatus { get; set; }
    }
}
