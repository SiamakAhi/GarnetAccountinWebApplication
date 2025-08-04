using GarnetAccounting.Areas.Treasury.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Treasury.Dto
{
    public class TreSandoghdarDto
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Display(Name = "شناسه فروشنده")]
        public long SellerId { get; set; }

        [Display(Name = "شناسه شخص")]
        public int PartyId { get; set; }
        public virtual Party Party { get; set; }

        [Display(Name = "شناسه صندوق")]
        public int SandoghId { get; set; }

        [Display(Name = "شناسه تفضیل")]
        public long TafzilId { get; set; }
    }
}
