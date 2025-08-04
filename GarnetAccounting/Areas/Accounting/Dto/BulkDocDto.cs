using GarnetAccounting.Areas.Accounting.Models.Entities;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class BulkDocDto
    {
        public List<Acc_Document> Headers { get; set; } = new List<Acc_Document>();
        public List<Acc_Article> Articles { get; set; } = new List<Acc_Article>();
        public bool Success { get; set; } = false;
        public string? Message { get; set; }
    }
}
