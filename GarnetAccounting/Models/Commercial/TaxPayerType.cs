namespace GarnetAccounting.Models.Commercial
{
    public class TaxPayerType
    {
        public Int16 Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Party> Parties { get; set; }
    }
}
