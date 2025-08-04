using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class DocumentsInfo
    {
        [Display(Name = "تعداد اسناد")]
        public int DocumentCount { get; set; }

        [Display(Name = "شماره آخرین سند")]
        public int LastDocumentNumber { get; set; }

        [Display(Name = "تعداد اسناد ناتراز")]
        public int UnbalancedDocumentCount { get; set; }

        [Display(Name = "جمع بدهکار")]
        public long TotalDebit { get; set; }

        [Display(Name = "جمع بستانکار")]
        public long TotalCredit { get; set; }

        [Display(Name = "بزرگترین تاریخ")]
        public DateTime? LatestDate { get; set; }

        [Display(Name = "ترتیب اسناد")]
        public bool IsSorted { get; set; }
    }
}
