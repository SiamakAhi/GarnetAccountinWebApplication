namespace GarnetAccounting.Areas.Accounting.Models.Entities
{
    public class Acc_Coding_Kol
    {
        public int Id { get; set; }
        public short TypeId { get; set; }
        public string KolCode { get; set; }
        public string KolName { get; set; }
        public string? Description { get; set; }
        public short Nature { get; set; }
        public long? SellerId { get; set; }
        public short GroupId { get; set; }
        public bool IsEditable { get; set; }
        public virtual Acc_Coding_Group KolGroup { get; set; }
        public virtual ICollection<Acc_Coding_Moein>? Moeins { get; set; }
    }
}
