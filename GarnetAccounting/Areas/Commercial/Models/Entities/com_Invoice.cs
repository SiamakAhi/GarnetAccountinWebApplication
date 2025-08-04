using GarnetAccounting.Areas.Projects.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Commercial.Models.Entities
{
    public class com_Invoice
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "شناسه فروشنده")]
        public long SellerId { get; set; }

        [Display(Name = "شناسه دوره مالی")]
        public int? FinancePeriodId { get; set; }

        //1- خرید
        //2- فروش
        //3- پیش فاکتور
        [Display(Name = "نوع فاکتور")]
        public short InvoiceType { get; set; }

        //1 اصلی
        //2 اصلاحی
        //3 ابطالی
        //4 برگشت از فروش
        [Display(Name = "موضوع")]
        public int? InvoiceSubject { get; set; }

        [Display(Name = "شماره فاکتور")]
        public string InvoiceNumber { get; set; }
        public string? TaxInvoiceNumber { get; set; }

        // InvoiceAutoNumber = [SellerId]&[LastSequenceNumber+1]
        public long InvoiceAutoNumber { get; set; }

        //LastSequenceNumber = LastSequenceNumber ++;
        public int SequenceNumber { get; set; }
        public string? ArchiveRef { get; set; }

        [Display(Name = "تاریخ فاکتور")]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "طرف حساب")]
        public long PartyId { get; set; }

        public long? Visitor { get; set; }
        public short? VisitorPercent { get; set; }

        [Display(Name = "نوع تسویه")]
        public int SettlementTypeId { get; set; }

        [Display(Name = "توضیحات")]
        public string? Remarks { get; set; }


        //1-جدید
        //2-در انتظار تسویه حساب
        //3- صدور حواله انبار
        //4-ابطال شده
        [Display(Name = "وضعیت")]
        public short status { get; set; }

        [Display(Name = "زمان ایجاد")]
        public DateTime CreationTime { get; set; }

        [Display(Name = " کاربر ایجاد کننده")]
        public string CreatorUserId { get; set; }

        [Display(Name = "آخرین ویرایش")]
        public DateTime? LastUpdate { get; set; }

        [Display(Name = " کاربر ویرایش کننده")]
        public string? EditorUserId { get; set; }
        // Navigation Properties
        public virtual ICollection<com_InvoiceItem> InvoiceItems { get; set; } = new List<com_InvoiceItem>();
        public virtual Party InvoiceParty { get; set; }
        public bool flag { get; set; } = false;

        [Display(Name = "مربوط به پروژه/قرارداد")]
        public int? projectId { get; set; }
        public virtual Con_Project? InvoiceProject { get; set; }

        public Guid? DocId { get; set; }
        public int? DocAutoNo { get; set; }
    }
}
