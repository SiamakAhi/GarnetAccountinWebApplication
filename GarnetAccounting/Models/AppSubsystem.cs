using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Models
{
    public class AppSubsystem
    {
        [Key]
        public int Id { get; set; }
        public string Name_fa { get; set; }
        public string Name_En { get; set; }
        public string? Description { get; set; }
    }
}
