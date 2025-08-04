using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Dto
{
    public class RateZoneDto
    {
        public int ZoneId { get; set; }

        public long SellerId { get; set; }

        [Required(ErrorMessage = "فیلد نام الزامی است.")]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "آیا اقماری است؟")]
        public bool IsSatellite { get; set; } = false;
    }
}