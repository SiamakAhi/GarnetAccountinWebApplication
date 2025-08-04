namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class ProfitReportSetting
    {
        public long SellerId { get; set; }
        public int PeriodId { get; set; }

        public List<int> IncomeGroups { get; set; }
        public List<int> IncomeKolAccounts { get; set; }

        public List<int> SaleGroups { get; set; }
        public List<int> SaleKolAccounts { get; set; }
        public List<int> SaleDiscountKolAccounts { get; set; }
        //
        public List<int> BuyGroups { get; set; }
        public List<int> BuyKolAccounts { get; set; }
        public List<int> BuyDiscountKolAccounts { get; set; }

        //
        public int MojoodiKalaMoeinId { get; set; }
        //
        public List<int> CostGroup { get; set; }
        public List<int> CostKolAccounts { get; set; }
        public List<int> GeneralCostKolAccounts { get; set; }

        public long? PayanDore { get; set; }
        public string? strPayanDore { get; set; }
        public bool payanDore_calcSystemic { get; set; }
        public string? startDate { get; set; }
        public string? endDate { get; set; }
    }
}
