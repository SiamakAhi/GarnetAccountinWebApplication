using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Dto
{
    public class RouteDto
    {
        public int RouteId { get; set; }
        public long SellerId { get; set; }

        [Display(Name = "کد مسیر")]
        [Required(ErrorMessage = "فیلد کد مسیر الزامی است")]
        public string RouteCode { get; set; }

        [Display(Name = "ترانزیتی/غیرترانزیتی")]
        [Required(ErrorMessage = "ترانزیتی/غیرترانزیتی الزامی است")]
        public bool IsTransit { get; set; } = false;

        [Required(ErrorMessage = "نام مسیر را وارد کنید")]
        [Display(Name = "نام مسیر")]
        public string RouteName { get; set; }

        [Display(Name = "نام لاتین مسیر")]
        public string? RouteName_En { get; set; }

        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "شهر مبدا را مشخص کنید")]
        [Display(Name = "مبدا")]
        public int OriginCityId { get; set; }

        [Display(Name = "مبدا")]
        public string? OriginCity { get; set; }

        [Required(ErrorMessage = "شهر مقصد را مشخص کنید")]
        [Display(Name = "مقصد")]
        public int DestinationCityId { get; set; }

        [Display(Name = "مقصد")]
        public string? DestinationCity { get; set; }

        [Display(Name = " zone")]
        public int? ZoneId { get; set; } = null;
        public string? ZoneName { get; set; } = "تعریف نشده";
    }

}
