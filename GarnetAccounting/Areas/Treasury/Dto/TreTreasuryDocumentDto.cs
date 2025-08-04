using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Treasury.Dto
{
    public class TreTreasuryDocumentDto
    {
        public int Id { get; set; }


        [Display(Name = "شناسه فروشنده")]
        public long SellerId { get; set; }

        [Display(Name = "شماره سند")]
        public string DocumentNumber { get; set; }

        [Display(Name = "تاریخ سند")]
        public DateTime DocumentDate { get; set; }

        [Display(Name = "مبلغ کل")]
        public long TotalAmount { get; set; }
    }
}
