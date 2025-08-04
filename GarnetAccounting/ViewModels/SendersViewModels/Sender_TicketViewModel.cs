using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.ViewModels.SendersViewModels
{
    public class Sender_TicketViewModel
    {
        [Display(Name = "شناسه تیکت")]
        public int TicketID { get; set; }

        [Display(Name = "مشتری")]
        public string CustomerName { get; set; }

        [Display(Name = "عنوان تیکت")]
        public string Title { get; set; }

        [Display(Name = "توضیحات تیکت")]
        public string Description { get; set; }

        [Display(Name = "ایجاد کننده")]
        public string CreatorName { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "زمان ایجاد")]
        public TimeSpan CreateTime { get; set; }
    }
}
