using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.ViewModels.Tax
{

    public class CsrInfoHoghooghi
    {

        // C
        [Display(Name = "کشور")]
        [Required(ErrorMessage = "")]
        public string Country { get; set; } = "IR";

        // O
        [Display(Name = "شخص حقوقی غیروابسته به دولت")]
        [Required(ErrorMessage = "")]
        public string Organization { get; set; } = "Non-Governmental";

        // OU
        [Display(Name = "نام شرکت مطابق با آگهی تأسیس")]
        [Required(ErrorMessage = "نام شرکت را مطابق با اساسنامه بنویسید")]
        public string OrganizationalUnit1 { get; set; }

        // OU
        [Display(Name = "نام  واحد سازمانی 1")]
        public string? OrganizationalUnit2 { get; set; }

        // OU
        [Display(Name = "نام  واحد سازمانی 2")]
        public string? OrganizationalUnit3 { get; set; }

        // CN
        [Display(Name = "نام شرکت به انگلیسی . بدون فاصله")]
        [Required(ErrorMessage = "نام شرکت را کامل و بدون فاصله به انگلیسی بنویسید")]
        public string CommonName { get; set; }  // نام خانوادگی انگلیسی بدون فاصله  

        // E
        [Display(Name = "پست الکترونیک")]
        public string? Email { get; set; }


        // SERIALNUMBER
        [Display(Name = "شناسه ملی 11 رقمی شرکت")]
        [Required(ErrorMessage = "شناسه ملی 11 رقمی شرکت را بدرستی وارد نمائید.")]
        public string? SerialNumber { get; set; }   //SERIALNUMBER


        // T: نقش یا سمت متقاضی در سازمان (اجباری برای Gov و NGO)
        public string? Title { get; set; }  // سمت سازمانی 

        //// S: استان (اجباری)
        //public string State { get; set; }  // نام استان

        //// L: شهرستان (اجباری)
        //public string Locality { get; set; }    // نام شهر

        // OrganizationIdentifier: شناسه سازمان (اجباری برای Gov و NGO)
        public string? OrganizationIdentifier { get; set; }  // شناسه سازمان
    }
}
