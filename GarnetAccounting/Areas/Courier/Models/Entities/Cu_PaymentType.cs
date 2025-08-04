using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Models.Entities
{
    public class Cu_PaymentType
    {
        [Key] // تعیین کلید اصلی
        public short Id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "فیلد نام الزامی است")]
        public string Name { get; set; }



    }
}
