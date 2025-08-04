using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Dto
{
    public class RateImpactTypeDto
    {
        public short Id { get; set; }

        [Display(Name = "کد تأثیر")]
        public Int16 RateImpactTypeCode { get; set; }

        [Required(ErrorMessage = "نام الزامی است.")]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }
    }
}