using GarnetAccounting.Areas.AvaRasta.Dto;
using GarnetAccounting.Areas.AvaRasta.Models.Entities;
using GarnetAccounting.Areas.CustomerArea.Dto;

namespace GarnetAccounting.Areas.AvaRasta.ViewModels
{
    public class CustomerManagementViewModel
    {
        public VmCustomer Customer { get; set; }
        public List<VmCustomerUsers>? CustomerUsers { get; set; }
        public List<License> Licenses { get; set; }
        public AddLicenseDto? NewLicense { get; set; }
    }

}

