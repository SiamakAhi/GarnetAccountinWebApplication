using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.ViewModels.IdentityViewModels
{
    public class VmLogin
    {
        [Required(ErrorMessage = "نام کاریری خود را وارد نمائید")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "کلمه عبور خود را وارد نمائید")]
        [Display(Name = "کلمه عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "مرا بخاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}
