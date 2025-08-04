using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Models.Entities
{
    public class Acc_DocStatus
    {
        [Key]
        public short Id { get; set; }
        public string Name { get; set; }
    }
}
