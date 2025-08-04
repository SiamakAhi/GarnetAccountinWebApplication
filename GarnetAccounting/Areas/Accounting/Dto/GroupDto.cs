using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class GroupDto
    {
        public short Id { get; set; }

        [Display(Name = "نوع حساب")]
        public short TypeId { get; set; }
        public short GroupType { get; set; }
        public string? TypeName { get; set; }
        public string? GroupTypeName { get; set; }

        [Display(Name = "کد حساب *")]
        [Required(ErrorMessage = "درج کد حساب الزامی است")]
        public string GroupCode { get; set; }

        [Display(Name = "نام حساب *")]
        [Required(ErrorMessage = "نام گروه را بنویسید")]
        public string GroupName { get; set; }

        [Display(Name = "شرح")]
        public string? Description { get; set; }

        [Display(Name = "فروشنده (شرکت)")]
        public long? SellerId { get; set; }

        public bool IsEditable { get; set; }
        public short Order { get; set; }

    }
}
