using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Warehouse.Models.Entities
{
    public class Wh_ProductCategory
    {
        public long CategoryId { get; set; }

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


        [Display(Name = "حساب معین")]
        public int? MoeinId { get; set; }
        [Display(Name = "حساب تفصیلی")]
        public long? TafsilId { get; set; }

        // Navigation Properties
        public Wh_ProductCategory? ParentCategory { get; set; }
        public ICollection<Wh_ProductCategory>? SubCategories { get; set; }
        public ICollection<Wh_Product> Products { get; set; }
    }

}
