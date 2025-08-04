using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Geolocation.Models.Entities
{
    public class Geo_Province
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "نام فارسی استان الزامی است.")]
        [MaxLength(100, ErrorMessage = "نام فارسی استان نمی‌تواند بیشتر از ۱۰۰ کاراکتر باشد.")]
        [Display(Name = "نام فارسی")]
        public string PersianName { get; set; }

        [Required(ErrorMessage = "نام انگلیسی استان الزامی است.")]
        [MaxLength(100, ErrorMessage = "نام انگلیسی استان نمی‌تواند بیشتر از ۱۰۰ کاراکتر باشد.")]
        [Display(Name = "نام انگلیسی")]
        public string EnglishName { get; set; }

        [Required(ErrorMessage = "کد یونیک استان الزامی است.")]
        [Display(Name = "کد یونیک")]
        public int UniqueCode { get; set; }

        [Required(ErrorMessage = "شناسه کشور الزامی است.")]
        [Display(Name = "کشور")]
        public int CountryId { get; set; }

        [Display(Name = "کشور")]
        public virtual Geo_Country Country { get; set; }
        public virtual ICollection<Geo_City> Cities { get; set; }
    }
}
