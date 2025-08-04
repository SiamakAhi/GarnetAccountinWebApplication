
using System.ComponentModel.DataAnnotations;
using GarnetAccounting.Classes.ValidationClasses;

namespace GarnetAccounting.ViewModels.Tax
{
    public class CsrInfoHaghighi
    {
        // C
        [Display(Name = "کشور")]
        [Required(ErrorMessage = "")]
        public string Country { get; set; } = "IR";
        // O
        [Display(Name = "شخص حقیقی مستقل")]
        [Required(ErrorMessage = "")]
        public string Organization { get; set; } = "Unaffiliated";  // Gov=Governmental , NGO=Non-Governmental ,Una =Unaffiliated
                                                                    // CN
        [Display(Name = "نام و نام خانوادگی انگلیسی بدون فاصله")]
        [Required(ErrorMessage = "نام و نام خانوادگی خود را بنویسید")]
        [EnglishNameWithoutSpace]
        public string CommonName { get; set; }  // نام خانوادگی انگلیسی بدون فاصله  
                                                // E
        [Display(Name = "پست الکترونیک")]
        public string? Email { get; set; }
        // SERIALNUMBER
        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "کد ملی را وارد نمایید.")]
        [PersonNationalCodeAttribute(ErrorMessage = ("کد ملی نامعتبر است"))]
        public string SerialNumber { get; set; }   //SERIALNUMBER
                                                   // SN
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "نام خانوادگی خود را وارد نمایید")]
        public string Surname { get; set; }   // نام خانوادگی فارسی
                                              // G
        [Display(Name = "نام")]
        //[Required(ErrorMessage = "نام خود را وارد نمایید")]
        public string GivenName { get; set; }  // نام فارسی
                                               // S: استان (اجباری)
                                               //[Display(Name = "استان")]
                                               ////[Required(ErrorMessage = "استان خود را بنویسید")]
                                               //public string? State { get; set; }  // نام استان

        //// L: شهرستان (اجباری)
        //[Display(Name = "شهر")]
        ////[Required(ErrorMessage = "شهرتان رو بنویسید.")]
        //public string? Locality { get; set; }    // نام شهر

    }
}
