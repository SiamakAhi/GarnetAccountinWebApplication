using GarnetAccounting.Areas.Treasury.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Treasury.Dto
{
    public class TreCheckBookDto
    {

        public int Id { get; set; }

        [Display(Name = "شناسه فروشنده")]
        public long SellerId { get; set; }


        [Display(Name = "تاریخ صدور")]
        public DateTime IssueDate { get; set; }

        [Display(Name = "شناسه حساب بانکی")]
        public int BankAccountId { get; set; }

        [Display(Name = "تعداد برگ")]
        public short BargNum { get; set; }

        [Display(Name = "سریال شروع")]
        public string SeryalStart { get; set; }

        [Display(Name = "سریال پایان")]
        public string SeryalEnd { get; set; }

        [Display(Name = "سری")]
        public string Seri { get; set; }
    }
}
