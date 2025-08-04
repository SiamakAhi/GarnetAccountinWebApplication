using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Models.Entities
{
    public class Cu_Representative
    {
        [Key]
        public Guid Id { get; set; }
        public long SellerId { get; set; }
        [Required]
        [Display(Name = "عنوان نمایندگی")]
        public string Title { get; set; }

        [Display(Name = "طرف حساب")]
        public long? TafsilId { get; set; }
        public long? PartyId { get; set; }
        public virtual Party? Person { get; set; }

        [Display(Name = "ملاحظات")]
        public string? Description { get; set; }
        public string? Avatar { get; set; }
        public string? AdditionalInfo { get; set; }
        public string? Address { get; set; }
        [Display(Name = "صادر کننده بارنامه")]
        public bool IsIssuer { get; set; } = false;
        [Display(Name = "توزیع کننده بار")]
        public bool IsDistributor { get; set; } = true;

        public virtual ICollection<Cu_Branch>? RepresentativeBranches { get; set; }

    }
}
