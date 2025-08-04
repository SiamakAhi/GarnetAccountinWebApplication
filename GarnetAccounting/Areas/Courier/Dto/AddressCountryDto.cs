using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Dto
{
    public class AddressCountryDto
    {
        [Display(Name = "شناسه کشور")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "نام فارسی کشور الزامی است")]
        [StringLength(100, ErrorMessage = "نام فارسی کشور نباید از 100 کاراکتر بیشتر باشد")]
        [Display(Name = "نام فارسی کشور")]
        public string NameFa { get; set; }

        [Required(ErrorMessage = "نام لاتین کشور الزامی است")]
        [StringLength(100, ErrorMessage = "نام لاتین کشور نباید از 100 کاراکتر بیشتر باشد")]
        [Display(Name = "نام لاتین کشور")]
        public string NameEn { get; set; }

        [Required(ErrorMessage = "نام مختصر کشور الزامی است")]
        [StringLength(5, MinimumLength = 2, ErrorMessage = "نام مختصر کشور باید بین 2 تا 5 کاراکتر باشد")]
        [Display(Name = "نام مختصر کشور")]
        public string Abbreviation { get; set; }

        public long SellerId { get; set; }
    }
}
