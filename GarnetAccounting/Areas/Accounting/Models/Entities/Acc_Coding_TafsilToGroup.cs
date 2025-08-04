namespace GarnetAccounting.Areas.Accounting.Models.Entities
{
    public class Acc_Coding_TafsilToGroup
    {
        public long Id { get; set; }
        public int GroupId { get; set; }
        public long TafsilId { get; set; }

        public virtual Acc_Coding_TafsilGroup Group { get; set; }
        public virtual Acc_Coding_Tafsil TafsilAccount { get; set; }
    }
}
