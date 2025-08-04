using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Dto.RepresentativeDtos
{
    public class RepresentativeDto
    {
        public Guid Id { get; set; }
        public long SellerId { get; set; }
        [Required]
        [Display(Name = "عنوان نمایندگی")]
        public string Title { get; set; }

        [Display(Name = "طرف حساب")]
        public long? PartyId { get; set; }
        public virtual Party? Person { get; set; }

        public long? TafsilId { get; set; }
        public string? TafsilName { get; set; }

        [Display(Name = "ملاحظات")]
        public string? Description { get; set; }
        public string? AdditionalInfo { get; set; }
        public IFormFile? AvatarFile { get; set; }
        public string? Avatar { get; set; }
    }
}
