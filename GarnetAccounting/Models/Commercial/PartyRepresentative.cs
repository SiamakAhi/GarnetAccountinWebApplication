namespace GarnetAccounting.Models.Commercial
{
    public class PartyRepresentative
    {
        public Int64 PartyId { get; set; }
        public virtual Party Party { get; set; }

        public Int64 RepresentativeId { get; set; }
        public virtual Party Representative { get; set; }
    }

}
