using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Treasury.Models.Entities
{
    public class TreChequeOperation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "نوع عملیات")]
        public string OperationType { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

    }
}
