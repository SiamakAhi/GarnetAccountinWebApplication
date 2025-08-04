namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class EndOfPeriodSettings
    {
        public int? PeriodId { get; set; }  // دوره مالی
        public long SellerId { get; set; }  // شرکت فعال
        public string? CurrentUser { get; set; }
        public int? SummaryAccountId { get; set; } // حساب خلاصه سود و زیان
        public int? RetainedEarningsAccountId { get; set; } // حساب سود و زیان انباشته
        public List<int>? IncomeAccountIds { get; set; } // سرفصل‌های درآمد
        public List<int>? ExpenseAccountIds { get; set; } // سرفصل‌های هزینه
        public int? PayanDoreAccount { get; set; } // حساب موجودی پایان دوره
        public int? MojoodiKalaAccount { get; set; } // حساب موجودی کالا 

        public int? EkhtetamiyeAccountId { get; set; } // حساب اختتامیه
        public string? strPayanDore { get; set; }
        public long payanDore { get; set; } = 0;

    }

}
