using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Dto
{
    public class ConsignmentNatureDto
    {
        public short Id { get; set; }

        public long SellerId { get; set; }

        [Required(ErrorMessage = "عنوان ماهیت الزامی است.")]
        [Display(Name = "عنوان ماهیت")]
        public string Name { get; set; }

        [Required(ErrorMessage = "کد ماهیت الزامی است.")]
        [Display(Name = "کد ماهیت")]
        public string Code { get; set; }

        [Display(Name = "امکان حمل هوایی")]
        public bool IsAirTransportable { get; set; }

        [Display(Name = "امکان حمل زمینی")]
        public bool IsGroundTransportable { get; set; }

        [Required(ErrorMessage = "نوع تأثیر بر نرخ پایه الزامی است.")]
        [Display(Name = "نوع تأثیر بر نرخ پایه")]
        public short RateImpactTypeId { get; set; }

        [Required(ErrorMessage = "مقدار تأثیر بر نرخ پایه الزامی است.")]
        [Display(Name = "مقدار تأثیر بر نرخ پایه")]
        public decimal RateImpactValue { get; set; }
    }
}