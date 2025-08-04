using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class ConProjectDto
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        public long SellerId { get; set; }

        [Display(Name = "نام پروژه")]
        [Required(ErrorMessage = "وارد کردن نام پروژه الزامی است")]
        public string ProjectName { get; set; }

        [Display(Name = "شماره قرارداد")]
        public string? ProjectNumber { get; set; }

        [Display(Name = "حساب تفصیلی")]
        public long? TafsilId { get; set; }

        [Display(Name = "تاریخ شروع پروژه")]
        public DateTime? ProjectStartDate { get; set; }

        [Display(Name = "تاریخ شروع پروژه")]
        public string? strDate { get; set; }

        [Display(Name = "مبلغ پروژه (ریال)")]
        public long? ProjectAmount { get; set; }

        [Display(Name = "مدت پیمان (روز)")]
        public int? ContractDurationDays { get; set; }

        [Display(Name = "کاربر ایجاد کننده")]
        [Required(ErrorMessage = "شناسه کاربر ایجاد کننده الزامی است")]
        public string CreatedBy { get; set; }

        [Display(Name = "کارفرما")]
        public long? ClientId { get; set; }
        public string? ClientName { get; set; }
        public bool IsActive { get; set; }
    }
}