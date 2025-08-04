using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class AccountTafsilDto
    {
        public int Id { get; set; }
        [Display(Name = "کد معین")]
        public string? MoeinCode { get; set; }

        [Display(Name = "نام حساب معین")]
        public string? MoeinName { get; set; }

        [Display(Name = "تفصیل سطح 4")]
        public int[]? Tafsil4_Array { get; set; }
        public string? Tafsil4_GroupIds { get; set; }

        [Display(Name = "تفصیل سطح 5")]
        public int[]? Tafsil5_Array { get; set; }
        public string? Tafsil5_GroupIds { get; set; }

        [Display(Name = "تفصیل سطح 6")]
        public int[]? Tafsil6_Array { get; set; }
        public string? Tafsil6_GroupIds { get; set; }

        [Display(Name = "تفصیل سطح 7")]
        public int[]? Tafsil7_Array { get; set; }
        public string? Tafsil7_GroupIds { get; set; }

        [Display(Name = "تفصیل سطح 8")]
        public int[]? Tafsil8_Array { get; set; }
        public string? Tafsil8_GroupIds { get; set; }

        public string? KolName { get; set; }
        public string? KolCode { get; set; }
        public int KolId { get; set; }
        public int? GroupId { get; set; }
        public string? GroupName { get; set; }
    }
}
