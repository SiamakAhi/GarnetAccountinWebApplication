using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Models.Entities
{
    public class Cu_FinancialTransactionType
    {
        [Key]
        public short Id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "فیلد نام نوع تراکنش الزامی است")]
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }
    }
}
