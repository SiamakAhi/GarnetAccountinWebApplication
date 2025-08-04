using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class MoeinDto
    {
        public int Id { get; set; }
        [Display(Name = "کد معین")]
        public string MoeinCode { get; set; }

        [Display(Name = "نام حساب معین")]
        public string MoeinName { get; set; }

        [Display(Name = "شرح")]
        public string? Description { get; set; }

        [Display(Name = "ماهیت")]
        public short Nature { get; set; }

        [Display(Name = "حساب ارزی")]
        public bool IsCurrencyAccount { get; set; }

        [Display(Name = "انتخاب ارز")]
        [AllowNull]
        public int? CurrencyId { get; set; }

        [Display(Name = "معین خلاف ماهیت")]
        [AllowNull]
        public int? MoeinContraryNatureId { get; set; }

        [Display(Name = "تفصیل سطح 4")]
        public string? Tafsil4_GroupIds { get; set; }

        [Display(Name = "تفصیل سطح 5")]
        public string? Tafsil5_GroupIds { get; set; }

        [Display(Name = "تفصیل سطح 6")]
        public string? Tafsil6_GroupIds { get; set; }

        [Display(Name = "تفصیل سطح 7")]
        public string? Tafsil7_GroupIds { get; set; }

        [Display(Name = "تفصیل سطح 8")]
        public string? Tafsil8_GroupIds { get; set; }

        [Display(Name = "فروشنده")]
        public long? SellerId { get; set; }

        [Display(Name = "قابل ویرایش است ؟")]
        public bool IsEditable { get; set; }

        [Display(Name = "نام سرفصل حساب")]
        public string? KolName { get; set; }
        public string? KolCode { get; set; }
        public int KolId { get; set; }
        public short? GroupId { get; set; }
        public string? GroupName { get; set; }
    }
}
