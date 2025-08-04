using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Treasury.Dto
{
    public class TreCarrencyDto
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Display(Name = "شناسه فروشنده")]
        public long SellerId { get; set; }

        [Display(Name = "نام کامل ارز")]
        public string FullName { get; set; }

        [Display(Name = "نام مختصر ارز")]
        public string ShortName { get; set; }

        [Display(Name = "قیمت ارز به ریال")]
        public decimal ExchangeRateToRial { get; set; }
    }
}
