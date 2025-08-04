using GarnetAccounting.Areas.AutoServiceAgency.Models.Entities;
using GarnetAccounting.Areas.Courier.Models.Entities;

namespace GarnetAccounting.Areas.Accounting.Models.Entities
{
    public class Acc_Coding_Tafsil
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string GroupsId { get; set; }
        public long? SellerId { get; set; }
        public bool IsPerson { get; set; }
        public virtual ICollection<Acc_Coding_TafsilToGroup>? TafsilToGroups { get; set; }
        public virtual ICollection<Cu_Branch>? Branches { get; set; }
        public virtual ICollection<asa_Contractor>? asaContractors { get; set; }

    }
}
