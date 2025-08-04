using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class DocDto
    {

        public Guid Id { get; set; }

        public long SellerId { get; set; }

        [Display(Name = "دوره مالی")]
        public int PeriodId { get; set; }

        [Display(Name = "نوع سند")]
        public short TypeId { get; set; }

        [Display(Name = "تاریح سند")]
        public DateTime? DocDate { get; set; }

        [Display(Name = "تاریح سند")]
        public string strDocDate { get; set; }

        [Display(Name = "شماره سند")]
        public int DocNumber { get; set; }

        [Display(Name = "شماره اتوماتیک")]
        public int AutoDocNumber { get; set; }

        [Display(Name = "شماره عطف")]
        public int AtfNumber { get; set; }

        [Display(Name = "شرح سند")]
        public string? Description { get; set; }

        [Display(Name = "مبلغ بدهکار")]
        public long Bed { get; set; }

        [Display(Name = "مبلغ بستانکار")]
        public long Bes { get; set; }

        [Display(Name = "اختلاف")]
        public long Ekhtelaf { get; set; }

        [Display(Name = "وضعیت سند")]
        public short StatusId { get; set; }

        [Display(Name = "زیرسیستم صادر کننده سند")]
        public int? SubsystemId { get; set; }

        [Display(Name = "شماره رفرنس سند مرتبط")]
        public long? SubsystemRef { get; set; }

        [Display(Name = "کاربر ایجاد کننده")]
        public string? CreatorUserName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? EditorUserName { get; set; }

        public DateTime? LastUpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? DeleteUser { get; set; }
        public bool IsDeleted { get; set; }

        public List<DocArticleDto>? Articles { get; set; }
    }
}
