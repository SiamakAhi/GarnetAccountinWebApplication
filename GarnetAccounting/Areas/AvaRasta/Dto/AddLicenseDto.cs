
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.AvaRasta.Dto
{
    public class AddLicenseDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "ماژول موردنظر را انتخاب کنید.")]
        public Guid ModuleId { get; set; }
        public DateTime PurchaseDate { get; set; }
        [Required(ErrorMessage = "تاریخ انقضای لایسنس را تعیین کنید.")]
        public DateTime ExpirationDate { get; set; }
    }

}
