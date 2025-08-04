using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Treasury.Dto
{
    public class TreBankTransactionDto
    {
        public long Id { get; set; }

        [Display(Name = "شناسه فروشنده")]
        public long SellerId { get; set; }

        [Display(Name = "شناسه حساب بانکی")]
        public long? BankAccountId { get; set; }

        [Display(Name = "بررسی شده")]
        public bool IsChecked { get; set; } = false;

        [Display(Name = "دارای سند")]
        public bool HasDoc { get; set; } = false;

        [Display(Name = "نام صاحب حساب")]
        public string? AccountHolderName { get; set; }

        [Display(Name = "ردیف")]
        public int? Row { get; set; }

        [Display(Name = "تاریخ")]
        public DateTime? Date { get; set; }

        [Display(Name = "شماره سند")]
        public string? DocumentNumber { get; set; }

        [Display(Name = "بدهکار")]
        public long? Debtor { get; set; } = 0;

        [Display(Name = "بستانکار")]
        public long? Creditor { get; set; } = 0;

        [Display(Name = "مانده")]
        public long? Balance { get; set; } = 0;

        [Display(Name = "شعبه")]
        public string? Branch { get; set; }

        [Display(Name = "ساعت")]
        public TimeSpan? Time { get; set; }

        [Display(Name = "شماره برگه/چک")]
        public string? DocumentOrCheckNumber { get; set; }

        [Display(Name = "شماره مشتری مرتبط")]
        public string? RelatedCustomerNumber { get; set; }

        [Display(Name = "نام مشتری مرتبط")]
        public string? RelatedCustomerName { get; set; }

        [Display(Name = "شرح")]
        public string? Description { get; set; }

        [Display(Name = "شناسه مرجع")]
        public string? ReferenceId { get; set; }

        [Display(Name = "یادداشت")]
        public string? Note { get; set; }

        [Display(Name = "کد شبا")]
        public string? IBAN { get; set; }

        [Display(Name = "واریزکننده/ذینفع")]
        public string? DepositorOrBeneficiary { get; set; }

        [Display(Name = "شناسه تراکنش")]
        public string? TransactionId { get; set; }


        [Display(Name = "کد عملیات")]
        public string? OperationCode { get; set; }

        [Display(Name = "عملیات")]
        public string? Operation { get; set; }
    }
}
