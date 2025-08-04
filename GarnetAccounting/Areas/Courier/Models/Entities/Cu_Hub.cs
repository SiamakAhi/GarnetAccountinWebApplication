using GarnetAccounting.Areas.Geolocation.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Models.Entities
{
    public class Cu_Hub
    {
        [Key]
        public Guid HubId { get; set; }
        public long SellerId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "نام هاب را وارد کنید")]
        public string HubName { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = "نام هاب را وارد کنید")]
        public int CityId { get; set; }
        public virtual Geo_City HubCity { get; set; }

        [Display(Name = "طول جغرافیایی")]
        public double Latitude { get; set; } = 0;

        [Display(Name = "عرض جغرافیایی")]
        public double Longitude { get; set; } = 0;

        [Display(Name = "آدرس هاب")]
        public string? HubAddress { get; set; }

        [Display(Name = "فعال است")]
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Cu_Branch>? Branches { get; set; }
        public virtual ICollection<Cu_BillOfLading>? BillOfLadings { get; set; }

    }
}
