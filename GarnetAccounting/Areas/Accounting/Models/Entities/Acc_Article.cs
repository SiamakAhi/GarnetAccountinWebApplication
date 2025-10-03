using GarnetAccounting.Areas.Projects.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Models.Entities
{
    public class Acc_Article
    {
        [Key]
        public Guid Id { get; set; }
        public int RowNumber { get; set; }
        public Guid DocId { get; set; }
        public long? SellerId { get; set; }
        public int PeriodId { get; set; }
        public int? KolId { get; set; }
        public int MoeinId { get; set; }
        public long Amount { get; set; }
        public long Bed { get; set; }
        public long Bes { get; set; }
        public string? Comment { get; set; }
        public long? Tafsil4Id { get; set; }
        public string? Tafsil4Name { get; set; }
        public long? Tafsil5Id { get; set; }
        public string? Tafsil5Name { get; set; }
        public long? Tafsil6Id { get; set; }
        public string? Tafsil6Name { get; set; }
        public long? Tafsil7Id { get; set; }
        public string? Tafsil7Name { get; set; }
        public long? Tafsil8Id { get; set; }
        public string? Tafsil8Name { get; set; }
        public string CreatorUserName { get; set; }
        public DateTime CreateDate { get; set; }
        public string? EditorUserName { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? DeleteUserName { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? ArchiveCode { get; set; }
        public Guid? InvoiceId { get; set; }
        public virtual Acc_Document Doc { get; set; }
        public virtual Acc_Coding_Moein Moein { get; set; }

        //جهت کنترل اسناد
        public bool IsInternalTransaction { get; set; } // تراکنش داخلی است. مثال : تأمین موجودی
        public bool IsChecked { get; set; } = false; //چک شده
        public string? AccountantRemark { get; set; }  // یادداشت حسابدار
        public DateTime? ReviewDate { get; set; }  // تاریخ بررسی مجدد

        public int? ProjectId { get; set; }
        public virtual Con_Project? Project { get; set; }

        public string? BatchNumber { get; set; }
        public long? BankTransactionId { get; set; }
        public long? MoadianId { get; set; }

        // Electronic book file
        public int? eBookFileId { get; set; }

    }
}
