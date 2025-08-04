using GarnetAccounting.Areas.Accounting.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Treasury.Dto
{
    public class TrePettyCashDto
    {
        public int Id { get; set; }


        [Display(Name = "شناسه فروشنده")]
        public long SellerId { get; set; }

        [Display(Name = "نام تنخواه دار")]
        public string TankhahName { get; set; }

        [Display(Name = "تلفن تنخواه دار")]
        public string TankhahPhone { get; set; }

        [Display(Name = "تاریخ شروع به تنخواه دار")]
        public DateTime StartDate { get; set; }

        [Display(Name = "شناسه معین")]
        public int MoeinId { get; set; }
      
        [Display(Name = "شناسه تفضیل")]
        public long TafzilId { get; set; }
    }
}
