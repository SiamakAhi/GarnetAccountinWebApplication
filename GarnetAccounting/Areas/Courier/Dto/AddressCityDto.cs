using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Dto
{
    public class AddressCityDto
    {
        [Display(Name = "شناسه شهر")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "نام فارسی شهر الزامی است")]
        [StringLength(100, ErrorMessage = "نام فارسی شهر نباید از 100 کاراکتر بیشتر باشد")]
        [Display(Name = "نام فارسی شهر")]
        public string NameFa { get; set; }

        [Required(ErrorMessage = "نام لاتین شهر الزامی است")]
        [StringLength(100, ErrorMessage = "نام لاتین شهر نباید از 100 کاراکتر بیشتر باشد")]
        [Display(Name = "نام لاتین شهر")]
        public string NameEn { get; set; }

        [StringLength(5, MinimumLength = 2, ErrorMessage = "نام مختصر شهر باید بین 2 تا 5 کاراکتر باشد")]
        [Display(Name = "نام مختصر شهر")]
        public string? Abbreviation { get; set; }

        [Required(ErrorMessage = "شناسه استان الزامی است")]
        [Display(Name = "شناسه استان")]
        public int StateId { get; set; }

        [Display(Name = "نام استان")]
        public string? StateName { get; set; }
        public int NeighborhoodCount { get; set; } = 0;

        public long SellerId { get; set; }
    }
}
