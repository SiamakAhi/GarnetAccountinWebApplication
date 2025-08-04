using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Geolocation.Models.Entities
{
    public class Geo_Country
    {
        public int Id { get; set; }

        [Display(Name = "نام فارسی")]
        [Required(ErrorMessage = "وارد کردن نام فارسی الزامی است")]
        [MaxLength(100, ErrorMessage = "حداکثر طول نام فارسی 100 کاراکتر است")]
        public string PersianName { get; set; }

        [Display(Name = "نام انگلیسی")]
        [Required(ErrorMessage = "وارد کردن نام انگلیسی الزامی است")]
        [MaxLength(100, ErrorMessage = "حداکثر طول نام انگلیسی 100 کاراکتر است")]
        public string EnglishName { get; set; }

        [Display(Name = "کد")]
        [Required(ErrorMessage = "وارد کردن کد الزامی است")]
        [MaxLength(10, ErrorMessage = "حداکثر طول کد 10 کاراکتر است")]
        public string Code { get; set; }

        [Display(Name = "کد عددی")]
        [Required(ErrorMessage = "وارد کردن کد عددی الزامی است")]
        public int NumericCode { get; set; }

        public virtual ICollection<Geo_Province> Provinces { get; set; } = new HashSet<Geo_Province>();
    }

}
