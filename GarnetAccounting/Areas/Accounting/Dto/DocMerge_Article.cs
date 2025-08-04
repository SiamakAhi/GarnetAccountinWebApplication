using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class DocMerge_Article
    {
        public Guid Id { get; set; }
        public Guid OldId { get; set; }

        [Display(Name = "شماره ردیف")]
        public int RowNumber { get; set; }

        [Display(Name = "شماره سند")]
        public int? DocNumber { get; set; }
        public DateTime? DocDate { get; set; }

        [Display(Name = "شماره سند")]
        public Guid DocId { get; set; }
        public Guid OldDocId { get; set; }

        [Display(Name = "دوره مالی")]
        public int PeriodId { get; set; }
        public long? SellerId { get; set; }

        [Display(Name = " حساب کل")]
        public int? KolId { get; set; }
        public string? KolName { get; set; }
        public string? GroupName { get; set; }

        [Display(Name = "حساب معین")]
        public int MoeinId { get; set; }
        public string? MoeinName { get; set; }
        public string? MoeinCode { get; set; }

        [Display(Name = "مبلغ جزء")]
        public long Amount { get; set; }

        [Display(Name = "مبلغ بدهکار")]
        public long Bed { get; set; }
        public string strBed { get; set; }

        [Display(Name = "بستانکار")]
        public long Bes { get; set; }
        public string strBes { get; set; }

        [Display(Name = "شرح آرتیکل")]
        public string? Comment { get; set; }

        [Display(Name = "تفصیلی سطح 4")]
        public long? Tafsil4Id { get; set; }

        [Display(Name = "تفصیلی سطح 4")]
        public string? Tafsil4Name { get; set; }

        [Display(Name = "تفصیلی سطح 5")]
        public long? Tafsil5Id { get; set; }

        [Display(Name = "تفصیلی سطح 5")]
        public string? Tafsil5Name { get; set; }

        [Display(Name = "تفصیلی سطح 6")]
        public long? Tafsil6Id { get; set; }

        [Display(Name = "تفصیلی سطح 6")]
        public string? Tafsil6Name { get; set; }

        [Display(Name = "تفصیلی سطح 7")]
        public long? Tafsil7Id { get; set; }

        [Display(Name = "تفصیلی سطح 7")]
        public string? Tafsil7Name { get; set; }

        [Display(Name = "")]
        public long? Tafsil8Id { get; set; }

        [Display(Name = "تفصیلی سطح 8")]
        public string? Tafsil8Name { get; set; }

        [Display(Name = "کابر ایجاد کننده")]
        public string? CreatorUserName { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreateDate { get; set; }
        public string? EditorUserName { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public bool IsDeleted { get; set; }

        [Display(Name = "کد بایگانی")]
        public string? ArchiveCode { get; set; }
        public bool IsMatch { get; set; }
    }
}
