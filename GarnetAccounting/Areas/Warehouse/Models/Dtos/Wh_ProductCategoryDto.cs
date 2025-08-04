using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Warehouse.Models.Dtos
{
    public class Wh_ProductCategoryDto
    {
        [Display(Name = "شناسه فروشنده")]
        public long SellerId { get; set; }

        [Display(Name = "نام دسته‌بندی")]
        public string CategoryName { get; set; }

        [Display(Name = "شناسه دسته‌بندی والد")]
        public long? ParentCategoryId { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        public long CategoryId { get; set; }
        public int Level { get; set; }
        public List<Wh_ProductCategoryDto> SubCategories { get; set; } = new List<Wh_ProductCategoryDto>();
    }
}