using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class DocMerge_Header
    {
        public Guid Id { get; set; }

        public long? SellerId { get; set; }

        [Display(Name = "دوره مالی")]
        public int? PeriodId { get; set; }

        [Display(Name = "نوع سند")]
        [Required(ErrorMessage = "نوع سند را انتخاب کنید")]
        public short TypeId { get; set; }

        [Display(Name = "تاریح سند")]
        public DateTime? DocDate { get; set; }

        [Display(Name = "تاریح سند")]
        [Required(ErrorMessage = "تاریخ سند را مشحص کنید")]
        public string strDocDate { get; set; }

        [Display(Name = "شماره سند")]
        [Required(ErrorMessage = "شماره سند الزامی است")]
        public int DocNumber { get; set; }


        [Display(Name = "شماره اتوماتیک")]
        public int? AutoDocNumber { get; set; }

        [Display(Name = "شماره عطف")]
        public int? AtfNumber { get; set; }

        [Display(Name = "شرح سند")]
        public string? Description { get; set; } = " بابت منظور نمودن مدارک مثبته ضمیمه سند حسابداری";

        [Display(Name = "زیرسیستم صادر کننده سند")]
        public int? SubsystemId { get; set; }

        [Display(Name = "شماره رفرنس سند مرتبط")]
        public long? SubsystemRef { get; set; }

        [Display(Name = "کاربر ایجاد کننده")]
        public string? CreatorUserName { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        //
        public List<Guid> DocsForMerge { get; set; }
        public bool MergeSameAccount { get; set; }
        public bool MergeSameTafsil { get; set; }
        public bool AddDocsMergedNumbetToDesc { get; set; } = true;

    }
}
