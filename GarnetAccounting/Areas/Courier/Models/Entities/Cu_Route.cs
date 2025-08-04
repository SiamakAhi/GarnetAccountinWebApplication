using GarnetAccounting.Areas.Geolocation.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Models.Entities
{
    public class Cu_Route
    {
        [Key] // تعیین کلید اصلی
        public int RouteId { get; set; }
        public long SellerId { get; set; }

        [Display(Name = "کد مسیر")]
        [Required(ErrorMessage = "فیلد کد مسیر الزامی است")]
        public string RouteCode { get; set; }

        [Display(Name = "نام مسیر")]
        [Required(ErrorMessage = "فیلد نام مسیر الزامی است")]
        public string RouteName { get; set; }

        [Display(Name = "نام لاتین مسیر")]
        [Required(ErrorMessage = "فیلد نام مسیر الزامی است")]
        public string? RouteName_En { get; set; }

        [Display(Name = "فعال/غیرفعال")]
        [Required(ErrorMessage = "فیلد فعال/غیرفعال الزامی است")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "ترانزیتی/غیرترانزیتی")]
        [Required(ErrorMessage = "ترانزیتی/غیرترانزیتی الزامی است")]
        public bool IsTransit { get; set; } = false;

        [Display(Name = "شناسه مبدا")]
        [Required(ErrorMessage = "فیلد شناسه مبدا الزامی است")]
        public int OriginCityId { get; set; }
        public virtual Geo_City OriginCity { get; set; }

        [Display(Name = "شناسه مقصد")]
        [Required(ErrorMessage = "فیلد شناسه مقصد الزامی است")]
        public int DestinationCityId { get; set; }
        public virtual Geo_City DestinationCity { get; set; }

        [Display(Name = "zone")]
        public int? ZoneId { get; set; }
        public virtual Cu_RateZone? Zone { get; set; }

        public virtual ICollection<Cu_BillOfLading> BillOfLadings { get; set; } = new List<Cu_BillOfLading>();
    }
}
