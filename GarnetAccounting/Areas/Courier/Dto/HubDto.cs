using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Dto
{
    public class HubDto
    {
        public Guid HubId { get; set; }
        public long SellerId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "نام هاب را وارد کنید")]
        public string HubName { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = "نام هاب را وارد کنید")]
        public int CityId { get; set; }
        public string? CityName { get; set; }

        [Display(Name = "طول جغرافیایی")]
        public double Latitude { get; set; } = 0;

        [Display(Name = "عرض جغرافیایی")]
        public double Longitude { get; set; } = 0;

        [Display(Name = "آدرس هاب")]
        public string? HubAddress { get; set; }

        [Display(Name = "فعال است")]
        public bool IsActive { get; set; } = true;
    }
}
