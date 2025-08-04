namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class CloseAccountArticleDto
    {

        public string? GroupAccountName { get; set; }


        public short Nature { get; set; }
        public short MandehNature { get; set; }
        public long? SellerId { get; set; }
        public int? PeriodId { get; set; }

        public int KolId { get; set; }
        public int MoeinId { get; set; }
        public string? MoeinName { get; set; }
        public string? MoeinCode { get; set; }

        public long Bed { get; set; }
        public long Bes { get; set; }
        public long Mandeh { get; set; }
    }
}
