using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Dto.RepresentativeDtos
{
    public class RepFilterDto
    {
        public long sellerId { get; set; }

        [Display(Name = "گروه بارنامه")]
        public int? BillOfLadingGroup { get; set; }

        [Display(Name = "نام نماینده")]
        public string? name { get; set; }

        [Display(Name = "از تاریخ")]
        public string? strStartDate { get; set; }

        [Display(Name = "تا تاریخ")]
        public string? strEndDate { get; set; }

        [Display(Name = "مقصد")]
        public string? Destination { get; set; }

        [Display(Name = "روش پرداخت")]
        public string? PaymentMethod { get; set; }

        [Display(Name = "صفحه جاری")]
        public int CurrentPage { get; set; }

        [Display(Name = "تعداد ردیف در هر صفحه")]
        public int PageSize { get; set; }
    }
}
