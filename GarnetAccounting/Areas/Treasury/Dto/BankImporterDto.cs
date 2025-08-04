using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Treasury.Dto
{
    public class BankImporterDto
    {
        public long SellerId { get; set; }

        [Display(Name = " انتخاب الگوی گزارش بانکی")]
        public int Pattern { get; set; }

        [Display(Name = " حساب بانکی")]
        public long BankAccountId { get; set; }

        [Display(Name = "انتخاب فایل گزارش با فرمت اکسل")]
        public IFormFile File { get; set; }
    }
}
