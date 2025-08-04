using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Models.Entities
{
    public class Acc_Coding_TafsilGroup
    {
        [Key]
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string? Description { get; set; }
        public long? SellerId { get; set; }
        public bool IsPerson { get; set; }
        public bool IsEditable { get; set; }

        public virtual ICollection<Acc_Coding_TafsilToGroup> TafsilToGroups { get; set; }
    }
}
