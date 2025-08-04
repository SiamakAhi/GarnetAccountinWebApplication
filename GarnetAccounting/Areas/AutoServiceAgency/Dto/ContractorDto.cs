using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.AutoServiceAgency.Dto
{
    public class ContractorDto
    {
        public int Id { get; set; }

        // نام پیمانکار
        [Display(Name = "نام پیمانکار", Description = "نام کامل پیمانکار")]
        public string Name { get; set; }
        public long? SellerId { get; set; }

        // حوزه مهارت (تخصص)
        [Display(Name = "حوزه مهارت", Description = "حوزه یا تخصص پیمانکار")]
        public string? SkillArea { get; set; }

        [Range(0, 100, ErrorMessage = "درصد سهم باید بین 0 تا 100 باشد.")]
        [Display(Name = "درصد سهم", Description = "درصدی که پیمانکار از اجرت کار دریافت می‌کند")]
        public decimal SharePercentage { get; set; } = 0;

        // فعال است
        [Display(Name = "وضعیت فعالیت", Description = "آیا پیمانکار فعال است؟")]
        public bool IsActive { get; set; } = true;

        // تاریخ ایجاد
        [Display(Name = "تاریخ ایجاد", Description = "تاریخ ثبت اطلاعات پیمانکار")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // ایجاد کننده
        [Display(Name = "ایجاد کننده", Description = "کاربری که اطلاعات پیمانکار را ثبت کرده است")]
        public string? CreatedBy { get; set; }

        [Display(Name = "حساب تفصیلی")]
        public long? TafsilId { get; set; }
        public string? TafsilName { get; set; }
    }
}
