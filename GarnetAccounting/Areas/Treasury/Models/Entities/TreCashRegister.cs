using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Treasury.Models.Entities
{
    public class TreCashRegister
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public long SellerId { get; set; }

        [Required(ErrorMessage = "نام صندوق الزامی است")]
        [Display(Name = "نام صندوق")]
        public string RegisterName { get; set; }

        [Display(Name = "محل فیزیکی صندوق")]
        public string? PhysicalLocation { get; set; }

        [Display(Name = "شناسه حساب معین")]
        public int? AccountId { get; set; }

        [Display(Name = "شناسه تفصیلی صندوق")]
        public long? DetailedAccountId { get; set; }

        [Required(ErrorMessage = "تاریخ افتتاح صندوق الزامی است")]
        [Display(Name = "تاریخ افتتاح صندوق")]
        public DateTime OpeningDate { get; set; }

        [Display(Name = "شناسه شعبه")]
        public Guid? BranchId { get; set; }
    }
}
