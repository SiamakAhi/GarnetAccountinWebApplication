using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Treasury.Dto
{
    public class TreTransactionDto
    {
        public int Id { get; set; }

        [Display(Name = "شناسه فاکتور")]
        public int InvoiceId { get; set; }


        [Display(Name = "نوع پرداخت")]
        public string Type { get; set; } // Cash, Check, Transfer

        [Display(Name = "مبلغ")]
        public long Amount { get; set; }

        [Display(Name = "تاریخ تراکنش")]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "شناسه چک")]
        public int? CheckId { get; set; } // برای پرداخت‌های چکی

        [Display(Name = "شناسه حساب بانکی")]
        public int BankAccountId { get; set; } // برای پرداخت‌های واریزی



        [Display(Name = "توضیحات")]
        public string Description { get; set; }
    }
}
