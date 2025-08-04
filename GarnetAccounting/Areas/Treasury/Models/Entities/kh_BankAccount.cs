using GarnetAccounting.Areas.Accounting.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Treasury.Models.Entities
{
    public class kh_BankAccount
    {
        [Key]
        public int Id { get; set; }
        public long SellerId { get; set; }
        public int? BankId { get; set; }
        public string? BranchCode { get; set; }
        public string? AccountName { get; set; }
        public string? AccountType { get; set; }
        public string? AccountNumber { get; set; }
        public string? SHABA { get; set; }
        public string? CardNumber { get; set; }
        public string? cvvt { get; set; }
        public string? CardDate { get; set; }
        public string? BankAddress { get; set; }
        public long? TafsilId { get; set; }
        public string? TafsilCode { get; set; }
        public int? MoeinId { get; set; }
        public bool IsActive { get; set; }
        public virtual kh_Bank? Bank { get; set; }

        public virtual Acc_Coding_Moein? Moein { get; set; }
    }
}
