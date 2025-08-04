namespace GarnetAccounting.Areas.Treasury.Models.Entities
{
    public class kh_Bank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long? TafsilId { get; set; }
        public string? TafsilCode { get; set; }
        public virtual ICollection<kh_BankAccount>? BankAccounts { get; set; }
    }
}
