using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.ViewModels.CommercialViewModel.ProductsAndServicesViewModels
{
    public class AddCategoryDto
    {
        [Display(Name = "نام دسته بندی")]
        [Required(ErrorMessage = "نوشتن نام دسته بندی الزامی است")]
        public string Name { get; set; }

        [Display(Name = "شــرح")]
        public string? Description { get; set; }

        [Display(Name = "دسته والد")]
        public int? ParentId { get; set; }
    }
}
