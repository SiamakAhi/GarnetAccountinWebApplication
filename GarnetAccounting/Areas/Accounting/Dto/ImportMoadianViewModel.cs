using GarnetAccounting.Areas.Accounting.Models.Entities;

namespace GarnetAccounting.Areas.Accounting.Dto
{
    public class ImportMoadianViewModel
    {
        public MoadianReportFilterDto filter { get; set; } = new MoadianReportFilterDto();
        public Pagination<Acc_MoadianReport> Data { get; set; }
    }
}
