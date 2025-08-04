using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Dto.RepresentativeDtos
{
    public class BranchDto
    {
        public Guid Id { get; set; }
        public long SellerId { get; set; }

        [Display(Name = "کد شعبه")]
        [Required(ErrorMessage = "کد غیر تکراری برای شعبه یا نمایندگی بنویسید")]
        public string? BranchCode { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "فیلد نام الزامی است")]
        public string BranchName { get; set; }

        [Display(Name = "شهر")]
        public int? CityId { get; set; }

        [Display(Name = "نوع مالکیت")]
        [Required(ErrorMessage = "فیلد نوع نمایندگی الزامی است")]
        public bool IsOwnership { get; set; } = true; // مالکیت/شراکنی

        [Display(Name = "تاریخ شروع به کار")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "درصد پورسانت")]
        public decimal? CommissionPercentage { get; set; }

        [Display(Name = "آیا هاب است؟")]
        public bool? IsHub { get; set; } = false;

        [Display(Name = "طول جغرافیایی")]
        public double? Latitude { get; set; }

        [Display(Name = "عرض جغرافیایی")]
        public double? Longitude { get; set; }

        [Display(Name = "نوع شعبه")]
        [Required(ErrorMessage = "فیلد نوع شعبه الزامی است")]
        public Int16 BranchTypeId { get; set; }

        [Display(Name = "تاریخ افتتاح")]
        public DateTime? OpeningDate { get; set; }

        [Display(Name = "تاریخ تمدید همکاری")]
        public DateTime? RenewalDate { get; set; }

        [Display(Name = " هاب")]
        public Guid? HubId { get; set; }

        [Display(Name = "نماینده")]
        public Guid? RepresentativeId { get; set; }

        // New fields added as requested
        [Display(Name = "آدرس")]
        public string? Address { get; set; }

        [Display(Name = "مسئول شعبه")]
        public string? BranchManager { get; set; }
        [Display(Name = "شماره تماس مسئول شعبه")]
        public string? BranchManagerPhoneNumber { get; set; }
        public long? TafsilId { get; set; }
        // فعالیت‌ها
        [Display(Name = "صادرکننده بارنامه")]
        public bool IsBillOfLadingIssuer { get; set; }

        [Display(Name = "ناوگان حمل درون شهری")]
        public bool IsUrbanFleet { get; set; }

        [Display(Name = "ناوگان حمل بین شهری زمینی")]
        public bool IsIntercityFleet { get; set; }
        [Display(Name = "تجزیه و مبادلات مرسولات")]
        public bool IsSortingAndExchange { get; set; }
        public string? Province { get; set; }
        public string? CityName { get; set; }
        public string? TafsilName { get; set; }

        [Display(Name = "بارنامه خارجی")]
        public bool IsExternalBLIssuer { get; set; }

        [Display(Name = "بارنامه داخلی")]
        public bool IsInternalBLIssuer { get; set; }

        [Display(Name = "نام شعبه در سیستم قدیم")]
        public string? OldBranchName { get; set; }

        [Display(Name = "نام نمایندگی در سیستم قدیم")]
        public string? OldDistRepName { get; set; }
    }
}

