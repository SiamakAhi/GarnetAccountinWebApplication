using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Dto
{
    public class AddressDto
    {
        public long AddressId { get; set; }

        [Required(ErrorMessage = " متن آدرس الزامی است")]
        [Display(Name = "آدرس")]
        public string AddressText { get; set; }

        [Display(Name = "تلفن ثابت")]
        public string? Landline { get; set; }

        [RegularExpression(@"^\d{11}$", ErrorMessage = "فرمت شماره موبایل نامعتبر است")]
        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = " شماره موبایل الزامی است")]
        public string Mobile { get; set; } = "";


        [Display(Name = "طول جغرافیایی")]
        public double Latitude { get; set; } = 0;

        [Display(Name = "عرض جغرافیایی")]
        public double Longitude { get; set; } = 0;

        [Required(ErrorMessage = " کدپستی الزامی است")]
        [Display(Name = "کد پستی")]
        public string PostalCode { get; set; } = "";

        [Display(Name = " پیش فرض برای فرستنده‌")]
        [Required(ErrorMessage = "فیلد پیش فرض برای فرستنده الزامی است")]
        public bool IsDefaultForSender { get; set; } = true;

        [Display(Name = " پیش فرض برای گیرنده‌")]
        [Required(ErrorMessage = "فیلد پیش فرض برای گیرنده الزامی است")]
        public bool IsDefaultForReceiver { get; set; } = true;

        [Display(Name = "شناسه محله")]
        [Required(ErrorMessage = " شناسه محله الزامی است")]
        public long NeighborhoodId { get; set; }


        [Required(ErrorMessage = " شناسه شخص الزامی است")]
        [Display(Name = "شناسه شخص")]
        public long? PersonId { get; set; }


        [Display(Name = "نام شخص")]
        public string PersonName { get; set; }

        public long SellerId { get; set; }
    }
}
