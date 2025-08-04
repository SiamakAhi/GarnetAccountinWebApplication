using GarnetAccounting.Models.Commercial;
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.AvaRasta.Models.Entities
{
    public class License
    {
        [Key]
        public Guid Id { get; set; }

        public int CustomerId { get; set; }
        public Guid ModuleId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Module Module { get; set; }
    }

}
