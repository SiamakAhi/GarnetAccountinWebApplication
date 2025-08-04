using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Treasury.Models.Entities
{
    public class TreDocumentType
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "نام نوع سند الزامی است")]
        [Display(Name = "نام نوع سند")]
        public string DocumentTypeName { get; set; }
    }
}
