using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.AvaRasta.ViewModels;
using GarnetAccounting.Areas.DataTransfer.Dto;
using GarnetAccounting.ViewModels.CommercialViewModel;

namespace GarnetAccounting.ViewModels
{
    public class HomePageViewModel
    {
        public VmCustomer? CustomerInfo { get; set; }
        public VmSellerDashboard? SellerDashboardData { get; set; }
        public DocDto_AddNew Doc { get; set; }
        public VmBillOfLandingMonitor? BillsMonitor { get; set; }
        public DocFilterDto filter { get; set; }
        public DocumentsInfo? DocumentsInfo { get; set; }
        public VmTafsilReport? TafsilReport { get; set; }
    }
}
