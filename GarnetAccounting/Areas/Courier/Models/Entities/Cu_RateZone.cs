using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Models.Entities
{
    public class Cu_RateZone
    {

        [Key] // تعیین کلید اصلی
        public int ZoneId { get; set; }
        public long SellerId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "فیلد نام الزامی است")]
        public string Name { get; set; }

        [Display(Name = "آیا اقماری است؟")]
        public bool IsSatellite { get; set; } = false;

        public virtual ICollection<Cu_Route> Cu_Routes { get; set; } = new List<Cu_Route>();

    }
}
