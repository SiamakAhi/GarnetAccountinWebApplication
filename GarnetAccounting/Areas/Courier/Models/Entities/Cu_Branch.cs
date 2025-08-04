using GarnetAccounting.Areas.Accounting.Models.Entities;
using GarnetAccounting.Areas.Geolocation.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Models.Entities
{
    public class Cu_Branch
    {
        [Key]
        public Guid Id { get; set; }
        public long SellerId { get; set; }

        [Display(Name = "کد شعبه")]
        public string? BranchCode { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "فیلد نام الزامی است")]
        public string BranchName { get; set; }

        [Display(Name = "شهر")]
        public int? CityId { get; set; }
        public virtual Geo_City? BranchCity { get; set; }

        [Display(Name = "نوع نمایندگی")]
        [Required(ErrorMessage = "فیلد نوع نمایندگی الزامی است")]
        public bool IsOwnership { get; set; } = true; // مالکیت/شراکنی

        [Display(Name = "تاریخ شروع به کار")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "طول جغرافیایی")]
        public double? Latitude { get; set; } = 0;

        [Display(Name = "عرض جغرافیایی")]
        public double? Longitude { get; set; } = 0;

        [Display(Name = "نوع شعبه")]
        [Required(ErrorMessage = "فیلد نوع شعبه الزامی است")]
        public Int16 BranchTypeId { get; set; }

        [Display(Name = "تاریخ افتتاح")]
        public DateTime? OpeningDate { get; set; }

        [Display(Name = "تاریخ تمدید همکاری")]
        public DateTime? RenewalDate { get; set; }

        [Display(Name = " هاب")]
        public Guid? HubId { get; set; }
        public virtual Cu_Hub? BranchHub { get; set; }

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
        public Acc_Coding_Tafsil? TafsilAccount { get; set; }

        // فعالیت‌های جدید
        [Display(Name = "صادرکننده بارنامه")]
        public bool IsBillOfLadingIssuer { get; set; }

        [Display(Name = "ناوگان حمل درون شهری")]
        public bool IsUrbanFleet { get; set; }

        [Display(Name = "ناوگان حمل بین شهری زمینی")]
        public bool IsIntercityFleet { get; set; }

        [Display(Name = "تجزیه و مبادلات مرسولات")]
        public bool? IsHub { get; set; } = false;

        [Display(Name = "درصد پورسانت")]
        public decimal? CommissionPercentage { get; set; }

        public virtual ICollection<Cu_BranchUser> BranchUsers { get; set; }


        // Fields with shorter names
        [Display(Name = "بارنامه خارجی")]
        public bool IsExternalBLIssuer { get; set; }

        [Display(Name = "بارنامه داخلی")]
        public bool IsInternalBLIssuer { get; set; }

        [Display(Name = "نام شعبه قدیم")]
        public string? OldBranchName { get; set; }

        [Display(Name = "نام نمایندگی قدیم")]
        public string? OldDistRepName { get; set; }

        public ICollection<Cu_BillOfLading> OriginBills { get; set; } = new List<Cu_BillOfLading>();

    }
}
