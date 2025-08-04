using GarnetAccounting.Areas.DataTransfer.Models;

namespace GarnetAccounting.Areas.DataTransfer.Dto
{
    public class SaleReportsViewModel
    {
        public List<KPOldSystemSaleReport>? Reports { get; set; }
        public SaleFilterDto? filter { get; set; }
    }
}
