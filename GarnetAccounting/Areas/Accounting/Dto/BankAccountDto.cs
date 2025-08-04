using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class BankAccountDto
    {
        public int Id { get; set; }
        public long SellerId { get; set; }

        [Display(Name = "بانک *")]
        [Required(ErrorMessage = "بانک را از لیست مربوطه انتخاب کنید")]
        public int? BankId { get; set; }
        public string? BankName { get; set; }

        [Display(Name = "کد/نام شعبه * ")]
        public string? BranchCode { get; set; }

        [Display(Name = "نام صاحب حساب *")]
        [Required(ErrorMessage = "نام صاحب حساب را بنویسید")]
        public string AccountName { get; set; }

        [Display(Name = "نوع حساب")]
        public string? AccountType { get; set; }

        [Display(Name = "شماره حساب *")]
        [Required(ErrorMessage = "شماره حساب را بنویسید")]
        public string? AccountNumber { get; set; }

        [Display(Name = "شماره شبا")]
        public string? SHABA { get; set; }

        [Display(Name = "شماره کارت فعال")]
        public string? CardNumber { get; set; }

        [Display(Name = "ccv2")]
        public string? cvvt { get; set; }

        [Display(Name = "تاریخ انقضای کارت")]
        public string? CardDate { get; set; }

        [Display(Name = "آدرس شعبه")]
        public string? BankAddress { get; set; }

        [Display(Name = "حساب فعال است")]
        public bool IsActive { get; set; }

        public long? TafsilId { get; set; }
        public int? MoeinId { get; set; }
        public string? TafsilCode { get; set; }
    }
}
