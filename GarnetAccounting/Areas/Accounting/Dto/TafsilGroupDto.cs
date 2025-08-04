using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class TafsilGroupDto
    {
        public int Id { get; set; }

        [Display(Name = "نام گروع تفصیلی")]
        public string GroupName { get; set; }

        [Display(Name = "شرح")]
        public string? Description { get; set; }

        [Display(Name = "فروشنده")]
        public long? SellerId { get; set; }

        [Display(Name = "شخص است ؟")]
        public bool IsPerson { get; set; }

        [Display(Name = "قابل ویرایش است ؟")]
        public bool IsEditable { get; set; }
    }
}
