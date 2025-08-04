using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Representatives.ViewModels
{
    public class Vm_BranchDashboard
    {
        [Display(Name = "تعداد بارنامه های جدید")]
        public int NewBillsCount { get; set; }

        [Display(Name = "تعداد توزیع شده امروز")]
        public int DistributedTodayCount { get; set; }

        [Display(Name = "در حال توزیع")]
        public int InDistributionCount { get; set; }

        [Display(Name = "تعداد بارنامه های مشکل دار")]
        public int ProblematicBillsCount { get; set; }

        [Display(Name = "بارنامه هایی که باید برگشت بخورند")]
        public int ReturnableBillsCount { get; set; }

        [Display(Name = "تعداد بارنامه های اعتباری")]
        public int CreditBillsCount { get; set; }

        [Display(Name = "تعداد بارنامه های نقدی")]
        public int CashBillsCount { get; set; }

        [Display(Name = "تعداد بارنامه های پسکرایه")]
        public int PostpaidBillsCount { get; set; }

        [Display(Name = "جمع مبالغ پسکرایه")]
        public decimal TotalPostpaidAmount { get; set; }

        [Display(Name = "تعداد بارنامه های توزیع نشده از روزهای قبل")]
        public int UndistributedPreviousBillsCount { get; set; }

        [Display(Name = "در انتظار تأیید پیک")]
        public int PendingCourierApprovalCount { get; set; }
    }
}
