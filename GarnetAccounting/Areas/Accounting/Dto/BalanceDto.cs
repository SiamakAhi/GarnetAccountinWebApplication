namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class BalanceDto
    {
        public string Section { get; set; }
        public int SectionOrder { get; set; }
        public string SubSection { get; set; }
        public string AccountName { get; set; }
        public long AccountAmount { get; set; }
        public long SubSectionTotal { get; set; }
        public long SectionTotal { get; set; }
        public bool UseInRight { get; set; }

    }
}
