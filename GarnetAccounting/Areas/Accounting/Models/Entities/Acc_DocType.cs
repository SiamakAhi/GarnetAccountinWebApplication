using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Areas.Accounting.Models.Entities
{
    public class Acc_DocType
    {
        [Key]
        public short Id { get; set; }
        public string DocTypeName { get; set; }
    }
}
