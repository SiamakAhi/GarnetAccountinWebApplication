using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.ViewModels.PartyDto
{
    public class SellerInfoDto
    {
        public long SellerId { get; set; }

        [Display(Name = "نام مدیرعامل")]
        public string? CEOName { get; set; }

        [Display(Name = "شماره تماس مدیرعامل")]
        public string? CEOContactNumber { get; set; }

        [Display(Name = "شماره پرونده مالیاتی")]
        public string? TaxFileNumber { get; set; }

        [Display(Name = "کد واحد مالیاتی")]
        public string? TaxUnitCode { get; set; }

        [Display(Name = "آدرس واحد مالیاتی")]
        public string? TaxUnitAddress { get; set; }

        [Display(Name = "ممیز مالیاتی")]
        public string? TaxAuditor { get; set; }

        [Display(Name = "رمز عبور پنل مالیاتی")]
        public string? TaxPanelPassword { get; set; }
    }
}
