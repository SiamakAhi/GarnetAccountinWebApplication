using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Dto
{
    public class AddressNeighborhoodDto
    {
        [Display(Name = "شناسه محله")]
        public long NeighborhoodId { get; set; }

        [Required(ErrorMessage = "نام فارسی محله الزامی است")]
        [StringLength(100, ErrorMessage = "نام فارسی محله نباید از 100 کاراکتر بیشتر باشد")]
        [Display(Name = "نام محله")]
        public string NameFa { get; set; }

        [StringLength(100, ErrorMessage = "نام لاتین محله نباید از 100 کاراکتر بیشتر باشد")]
        [Display(Name = "نام لاتین محله")]
        public string NameEn { get; set; }

        [Required(ErrorMessage = "شناسه شهر الزامی است")]
        [Display(Name = "شناسه شهر")]
        public int CityId { get; set; }


        [Display(Name = "شماره منطقه")]
        public byte? DistrictNo { get; set; }


        [Display(Name = "نام شهر")]
        public string CityName { get; set; } = "";


        [Display(Name = "نام استان")]
        public string StateName { get; set; } = "";
        public int AddressCount { get; set; } = 0;

        public long SellerId { get; set; }
    }
}
