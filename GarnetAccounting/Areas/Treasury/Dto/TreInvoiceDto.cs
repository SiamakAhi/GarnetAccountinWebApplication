using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Treasury.Dto
{
    public class TreInvoiceDto
    {
        public int Id { get; set; }

        [Display(Name = "شناسه فروشنده")]
        public long SellerId { get; set; }

        [Display(Name = "مبلغ کل")]
        public long TotalAmount { get; set; }

        [Display(Name = "مبلغ پرداخت شده")]
        public long PaidAmount { get; set; }
    }
}
