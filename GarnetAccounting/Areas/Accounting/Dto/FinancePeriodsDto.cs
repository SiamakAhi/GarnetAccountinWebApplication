using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class FinancePeriodsDto
    {
        public int Id { get; set; }

        [Display(Name = "نام دوره (سال)")]
        [Required(ErrorMessage = "نام دوره مالی را بنویسید")]
        public string Name { get; set; }

        [Display(Name = "شروع دوره")]

        public DateTime StartDate { get; set; }

        [Display(Name = "شروع دوره")]
        [Required(ErrorMessage = "تاریخ شروع دوره باشد مشخص باشد")]
        public string? strStartDate { get; set; }

        [Display(Name = "پایان دوره")]

        public DateTime EndDate { get; set; }

        [Display(Name = "پایان دوره")]
        [Required(ErrorMessage = "تاریخ پایان دوره باید مشخص باشد")]
        public string? strEndDate { get; set; }

        [Display(Name = "نرخ مالیات بر ارزش افزوده")]
        public decimal? DefualtVatRate { get; set; }

        [Display(Name = "شرح")]
        public string? Description { get; set; }

        [Display(Name = "قروشنده")]
        public long? SellerId { get; set; }
    }
}
