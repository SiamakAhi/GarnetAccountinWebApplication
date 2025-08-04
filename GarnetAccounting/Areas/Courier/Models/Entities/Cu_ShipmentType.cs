using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Courier.Models.Entities
{
    public class Cu_ShipmentType
    {
        [Key]
        public Int16 Id { get; set; }
        public Int16 Code { get; set; }
        public string Name { get; set; }

    }
}
