using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Models.Entities
{
    public class Acc_FinancialPeriod
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? DefualtVatRate { get; set; }
        public string? Description { get; set; }
        public long? SellerId { get; set; }

        public virtual ICollection<Acc_Document> Documents { get; set; }
    }
}
