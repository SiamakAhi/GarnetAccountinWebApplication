using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Treasury.Models.Entities
{
    public class TreOperation
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "نوع عملیات")]
        public short OperationType { get; set; }

        [Required(ErrorMessage = "نام عملیات الزامی است")]
        [Display(Name = "نام عملیات")]
        public string OperationName { get; set; }

        [Display(Name = "تراکنش از طریق POS")]
        public bool IsPOSTransaction { get; set; } // برای نشان دادن اینکه تراکنش از طریق POS است
    }
}
