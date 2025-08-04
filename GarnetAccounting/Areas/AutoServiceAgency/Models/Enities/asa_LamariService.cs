using GarnetAccounting.Areas.AutoServiceAgency.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.AutoServiceAgency.Models.Enities
{
    public class asa_LamariService
    {
        [Key]
        public int Id { get; set; }
        public long SellerId { get; set; }

        // 1 > سرویس دوره ای
        // 2 > سایر خدمات
        [Required(ErrorMessage = "نوع سرویس را مشخص کنید")]
        [Display(Name = "نوع سرویس")]
        public short ServiceType { get; set; }


        [Required(ErrorMessage = "نام سرویس را بنویسید")]
        [Display(Name = "نام سرویس")]
        public string ServiceName { get; set; }


        [Required(ErrorMessage = "حساب معین سرویس را مشخص کنید")]
        [Display(Name = "انتخاب حساب معین")]
        public int MoeinId { get; set; }

        [Display(Name = "سهم مکانیک")]
        public long MechanicShareAmount { get; set; } = 0;

        [Display(Name = "سهم برقکار")]
        public long ElectricianShareAmount { get; set; } = 0;

        [Display(Name = "سهم نمایندگی")]
        public long AgencySharePercentage { get; set; } = 0;

        [Display(Name = "انتخاب حساب تفصیلی (درصورت لزوم)")]
        public long? TafsilId { get; set; }

        [Display(Name = "وضعیت سرویس")]
        public bool IsActive { get; set; } = true;

        public virtual ICollection<asa_Reception>? Receptions { get; set; }
    }
}
