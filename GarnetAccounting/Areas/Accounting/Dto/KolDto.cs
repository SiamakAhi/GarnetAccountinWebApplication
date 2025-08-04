using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class KolDto
    {
        public int Id { get; set; }

        [Display(Name = "نوع حساب")]
        [Required(ErrorMessage = "نوع حساب را انتخاب کنید")]
        public short TypeId { get; set; }
        public string? TypeName { get; set; }

        [Display(Name = "کد حساب")]
        [Required(ErrorMessage = "درج کد حساب الزامی است.")]
        public string KolCode { get; set; }

        [Display(Name = "نام حساب")]
        [Required(ErrorMessage = "درج نام حساب الزامی است.")]
        public string KolName { get; set; }

        [Display(Name = "شرح حساب")]
        public string? Description { get; set; }

        [Display(Name = "ماهیت")]
        [Required(ErrorMessage = "ماهیت حساب را انتخاب کنید")]
        public short Nature { get; set; }
        public string? NatureName { get; set; }
        public long? SellerId { get; set; }

        [Display(Name = "گروه حساب")]
        public short GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? GroupCode { get; set; }
        public bool IsEditable { get; set; }
    }
}
