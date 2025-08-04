using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Models.Entities
{
    public class Cu_FinancialTransaction
    {
        [Key]
        public Guid Id { get; set; }
        public short TransactionTypeId { get; set; }

        public int MyProperty { get; set; }
    }
}
