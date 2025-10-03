using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Models.Entities
{
    public class Acc_Document
    {
        [Key]
        public Guid Id { get; set; }
        public long SellerId { get; set; }
        public int PeriodId { get; set; }
        public short TypeId { get; set; }
        public DateTime DocDate { get; set; }
        public int DocNumber { get; set; }
        public int AutoDocNumber { get; set; }
        public int AtfNumber { get; set; }
        public string? Description { get; set; }
        public short StatusId { get; set; }
        public int? SubsystemId { get; set; }
        public long? SubsystemRef { get; set; }
        public string CreatorUserName { get; set; }
        public DateTime CreateDate { get; set; }
        public string? EditorUserName { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? DeleteUserName { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual Acc_FinancialPeriod DocPeriod { get; set; }
        public virtual ICollection<Acc_Article> DocArticles { get; set; }

        // Electronic book file
        public int? eBookFileId { get; set; }
    }
}
