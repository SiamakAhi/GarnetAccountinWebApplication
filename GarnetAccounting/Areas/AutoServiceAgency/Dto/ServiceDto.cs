using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.AutoServiceAgency.Dto
{
    public class ServiceDto
    {

        public int Id { get; set; }
        public long SellerId { get; set; }

        [Display(Name = "نام سرویس")]
        public string ServiceName { get; set; }

        [Display(Name = "حساب معین درآمد")]
        [Required(ErrorMessage = "حساب معین مربوط به سرویس موردنظر را انتخاب کنید")]
        public int MoeinId { get; set; }

        [Display(Name = "تفصیل سرویس (درصورت لزوم)")]
        public long? TafsilId { get; set; }

        [Display(Name = "وضعیت (قابل ارایه است ؟)")]
        public bool IsActive { get; set; } = true;
    }
}
