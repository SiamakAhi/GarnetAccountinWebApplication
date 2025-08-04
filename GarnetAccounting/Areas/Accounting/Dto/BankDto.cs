using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class BankDto
    {
        public int Id { get; set; }
        [Display(Name = "نام بانک")]
        [Required(ErrorMessage = "نام بانک را بنویسید.")]
        public string Name { get; set; }

        [Display(Name = "کد تفصیلی")]
        public long? TafsilId { get; set; }
        [Display(Name = "کد تفصیلی")]
        public string? TafsilCode { get; set; }
    }
}
