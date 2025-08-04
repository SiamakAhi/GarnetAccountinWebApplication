using GarnetAccounting.Areas.Treasury.Models.Entities;

namespace GarnetAccounting.Areas.Accounting.Models.Entities
{
    public class Acc_Coding_Moein
    {
        public int Id { get; set; }
        public string MoeinCode { get; set; }
        public string MoeinName { get; set; }
        public string? Description { get; set; }
        public short Nature { get; set; }
        public bool IsCurrencyAccount { get; set; }
        public int? CurrencyId { get; set; }
        public int? MoeinContraryNatureId { get; set; }
        public string? Tafsil4_GroupIds { get; set; }
        public string? Tafsil5_GroupIds { get; set; }
        public string? Tafsil6_GroupIds { get; set; }
        public string? Tafsil7_GroupIds { get; set; }
        public string? Tafsil8_GroupIds { get; set; }
        public long? SellerId { get; set; }
        public bool IsEditable { get; set; }
        public int KolId { get; set; }
        public virtual Acc_Coding_Kol MoeinKol { get; set; }
        public virtual ICollection<Acc_Article> Articles { get; set; }
        public virtual ICollection<kh_BankAccount>? BankAccounts { get; set; }
    }
}
