using GarnetAccounting.Areas.AutoServiceAgency.Models.Entities;

namespace GarnetAccounting.Areas.AutoServiceAgency.Models.Enities
{
    public class asa_Services
    {
        public int Id { get; set; }
        public long SellerId { get; set; }
        public string ServiceName { get; set; }
        public int MoeinId { get; set; }
        public long? TafsilId { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual ICollection<asa_Reception>? Receptions { get; set; }
    }
}
