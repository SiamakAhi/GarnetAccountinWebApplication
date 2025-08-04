namespace GarnetAccounting.ViewModels.CommercialViewModel.ProductsAndServicesViewModels
{
    public class StuffFilterDto
    {

        public Int64? stuffId { get; set; }
        public bool IsService { get; set; }
        public string? name { get; set; }
        public string? taxable { get; set; }
        public string? type { get; set; }
        public string? Code { get; set; }

    }
}
