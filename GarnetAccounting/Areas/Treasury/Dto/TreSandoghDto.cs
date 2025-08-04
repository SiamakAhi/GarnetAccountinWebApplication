using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Treasury.Dto
{
    public class TreSandoghDto
    {
        public int Id { get; set; }


        [Display(Name = "شناسه فروشنده")]
        public long SellerId { get; set; }


        [Display(Name = "نام صندوق")]
        public string Name { get; set; }

        [Display(Name = "تاریخ افتتاح")]
        public DateTime OpeningDate { get; set; }

        [Display(Name = "موجودی اولیه")]
        public decimal? InitialBalance { get; set; }

        [Display(Name = "شناسه معین")]
        public int? MoeinId { get; set; }
    }
}
