namespace GarnetAccounting.ViewModels.Tax
{
    public class CsrInformation
    {
        // C: کشور (اجباری) - مقادیر ممکن: Gov, NGO, Una
        public string Country { get; set; } = "IR";

        // O: سازمان (اجباری) - مقادیر ممکن: NGO, Una
        public string Organization { get; set; }  // Gov=Governmental , NGO=Non-Governmental ,Una =Unaffiliated

        // OU: نام سازمان / واحد سازمانی فارسی (اجباری برای Gov و NGO)
        public string OrganizationalUnit1 { get; set; }

        // OU: واحد سازمانی 2 (اختیاری)
        public string? OrganizationalUnit2 { get; set; }

        // OU: واحد سازمانی 3 (اختیاری)
        public string? OrganizationalUnit3 { get; set; }

        // CN: نام مشترک / نام خانوادگی [علامت] (اجباری)
        public string CommonName { get; set; }  // نام خانوادگی انگلیسی بدون فاصله  

        // E: آدرس ایمیل (اختیاری)
        public string Email { get; set; }

        // SERIALNUMBER: کد ملی متقاضی (اجباری)
        public string SerialNumber { get; set; }   //SERIALNUMBER

        // SN: نام خانوادگی متقاضی (اجباری)
        public string Surname { get; set; }   // نام خانوادگی فارسی

        // G: نام متقاضی (اجباری)
        public string GivenName { get; set; }  // نام فارسی

        // T: نقش یا سمت متقاضی در سازمان (اجباری برای Gov و NGO)
        public string Title { get; set; }  // سمت سازمانی 

        // S: استان (اجباری)
        public string State { get; set; }  // نام استان

        // L: شهرستان (اجباری)
        public string Locality { get; set; }    // نام شهر

        // OrganizationIdentifier: شناسه سازمان (اجباری برای Gov و NGO)
        public string OrganizationIdentifier { get; set; }  // شناسه سازمان
    }
}
