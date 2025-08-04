using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Treasury.Models.Entities
{
    public class TreBankPosUc
    {
        [Key]
        [Display(Name = "شناسه")]
        public long Id { get; set; }

        [Required(ErrorMessage = "نام الزامی است")]
        [StringLength(50, ErrorMessage = "نام نباید بیشتر از 50 کاراکتر باشد")]
        [Display(Name = "نام")]
        public string Name { get; set; }
    }
}
