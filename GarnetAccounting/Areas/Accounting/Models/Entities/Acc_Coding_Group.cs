namespace GarnetAccounting.Areas.Accounting.Models.Entities
{
    public class Acc_Coding_Group
    {
        public short Id { get; set; }
        public short TypeId { get; set; }
        public short GroupType { get; set; }
        public short Order { get; set; }
        public string GroupCode { get; set; }
        public string? AltGroupCode { get; set; }
        public string GroupName { get; set; }
        public string? Description { get; set; }
        public long? SellerId { get; set; }
        public bool IsEditable { get; set; }
        public virtual ICollection<Acc_Coding_Kol>? Kols { get; set; }

    }
}
