using GarnetAccounting.Areas.Courier.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarnetAccounting.Areas.Geolocation.Models.Entities
{
    public class Geo_City
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "نام فارسی")]
        [Required(ErrorMessage = "وارد کردن نام فارسی الزامی است")]
        [MaxLength(100, ErrorMessage = "نام فارسی نمی‌تواند بیش از 100 کاراکتر باشد")]
        public string PersianName { get; set; }

        [Display(Name = "نام انگلیسی")]
        [Required(ErrorMessage = "وارد کردن نام انگلیسی الزامی است")]
        [MaxLength(100, ErrorMessage = "نام انگلیسی نمی‌تواند بیش از 100 کاراکتر باشد")]
        public string EnglishName { get; set; }

        [Display(Name = "کد یکتا")]
        [Required(ErrorMessage = "وارد کردن کد یکتا الزامی است")]
        public int UniqueCode { get; set; }

        public string? IATACode { get; set; }

        [Display(Name = "استان")]
        [Required(ErrorMessage = "وارد کردن شناسه استان الزامی است")]
        public int ProvinceId { get; set; }

        [ForeignKey("ProvinceId")]
        public virtual Geo_Province Province { get; set; }

        [Display(Name = "مرکز استان")]
        public bool IsCapital { get; set; }

        public virtual ICollection<Cu_Branch> CityBranches { get; set; } = new List<Cu_Branch>();
        public virtual ICollection<Cu_Hub> CityHubs { get; set; } = new List<Cu_Hub>();
        public virtual ICollection<Cu_Route> OriginCities { get; set; } = new List<Cu_Route>();
        public virtual ICollection<Cu_Route> DestinationCities { get; set; } = new List<Cu_Route>();
    }
}
