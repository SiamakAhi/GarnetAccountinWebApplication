using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Dto
{
    public class AddressStateDto
    {
        [Display(Name = "شناسه استان")]
        public int StateId { get; set; }

        [Required(ErrorMessage = "نام فارسی استان الزامی است")]
        [StringLength(100, ErrorMessage = "نام فارسی استان نباید از 100 کاراکتر بیشتر باشد")]
        [Display(Name = "نام فارسی استان")]
        public string NameFa { get; set; }

        [Required(ErrorMessage = "نام لاتین استان الزامی است")]
        [StringLength(100, ErrorMessage = "نام لاتین استان نباید از 100 کاراکتر بیشتر باشد")]
        [Display(Name = "نام لاتین استان")]
        public string NameEn { get; set; }



        [Required(ErrorMessage = "شناسه کشور الزامی است")]
        [Display(Name = "شناسه کشور")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "نام کشور الزامی است")]
        [Display(Name = "نام کشور")]
        public string CountryName { get; set; }

        public long SellerId { get; set; }
    }
}
