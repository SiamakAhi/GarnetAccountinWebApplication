using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.ViewModels.IdentityViewModels
{
    public class VmUpdateProfile
    {
        public string Id { get; set; }

        [Display(Name = "نام کاربری")]
        public string userName { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "شماره موبایل")]
        public string Mobile { get; set; }

        [Display(Name = "شماره تلفن")]
        public string PhoneNumber { get; set; }

        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Display(Name = "تاریخ تولد")]
        public DateTime? BirthDate { get; set; }
        public DateTime RegisterDate { get; set; }

        [Display(Name = "تاریخ تولد")]
        public string? StrBirthDate { get; set; }

        [Display(Name = "تصویر پروفایل")]
        public string? Image { get; set; }

        [Display(Name = "تصویر پروفایل")]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "جنسیت")]
        public Int16? Gender { get; set; }

        public int? PersonId { get; set; }
    }
}
