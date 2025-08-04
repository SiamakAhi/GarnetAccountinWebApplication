using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.ViewModels.IdentityViewModels
{
    public class AppRolViewModel
    {
        [Display(Name = "شناسه")]
        public string? Id { get; set; }

        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "کاربران")]
        public int UsersCount { get; set; }
    }

}
