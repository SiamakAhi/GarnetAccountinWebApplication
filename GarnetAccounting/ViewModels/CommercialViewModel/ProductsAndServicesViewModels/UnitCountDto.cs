namespace GarnetAccounting.ViewModels.CommercialViewModel.ProductsAndServicesViewModels
{
    public class UnitCountDto
    {
        public VmUnitOfMeasurement UnitCount { get; set; }
        public Pagination<VmUnitOfMeasurement>? UnitList { get; set; }
    }
}
