using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.ViewModels.IdentityViewModels
{
    public class VmChangePass
    {
        public string UserName { get; set; }

        [Display(Name = "پسورد قدیم")]
        public string OldPassword { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "پسورد جدید")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        [Display(Name = "تأیید پسورد جدید")]
        public string ConfirmPassword { get; set; }
    }
}
