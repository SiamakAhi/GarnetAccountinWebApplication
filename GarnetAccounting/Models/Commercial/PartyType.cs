using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Models.Commercial
{
    public class PartyType
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
