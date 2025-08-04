using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.ViewModels.CommercialViewModel.ProductsAndServicesViewModels
{
    public class VmUnitOfMeasurement
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "نام واحد اندازه گیری را وارد نماید")]
        public string Name { get; set; }

        [Display(Name = "کد")]
        [Required(ErrorMessage = "ورود کد الزامی ست")]
        public string? Code { get; set; }
    }
}
