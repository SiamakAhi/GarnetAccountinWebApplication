using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Treasury.Models.Enums
{
    public enum TreOperationType
    {
        [Display(Name = "دريافت نقدي")]
        CashReceipt = 1,

        [Display(Name = "پرداخت نقدي")]
        CashPayment = 2,

        [Display(Name = "پرداخت چك")]
        ChequePayment = 3,

        [Display(Name = "دريافت چک و سفته")]
        ChequeAndPromissoryNoteReceipt = 4,

        [Display(Name = "عمليات چکها و سفته هاي دريافتي")]
        ReceivedChequesAndNotesOperations = 5,

        [Display(Name = "عمليات چكهاي دريافتي")]
        ReceivedChequesOperations = 6,

        [Display(Name = "صدور ضمانتنامه")]
        IssueGuarantee = 7,

        [Display(Name = "دريافت ضمانتنامه")]
        ReceiveGuarantee = 8,

        [Display(Name = "عمليات ضمانتنامه هاي پرداختي")]
        PaidGuaranteeOperations = 9,

        [Display(Name = "عمليات ضمانتنامه هاي دريافتي")]
        ReceivedGuaranteeOperations = 10,

        [Display(Name = "ثبت وام هاي دريافتي (پرداختني)")]
        RegisterReceivedLoans = 11,

        [Display(Name = "فرم پرداخت اقساط تسهيلات")]
        InstallmentPaymentForm = 12,

        [Display(Name = "اسناد تنخواه")]
        PettyCashDocuments = 13,

        [Display(Name = "برداشت بانک")]
        BankWithdrawal = 14,

        [Display(Name = "واريز به حساب")]
        AccountDeposit = 15,

        [Display(Name = "دريافت اسناد خزانه")]
        TreasuryDocumentsReceipt = 16
    }
}
