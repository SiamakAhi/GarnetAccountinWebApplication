namespace GarnetAccounting.ViewModels.CommercialViewModel.ProductsAndServicesViewModels
{
    public class VmProductOrServiceInfo
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public decimal? Fee { get; set; }
        public float? VatRate { get; set; }
        public string UnitcountCode { get; set; }
        public string UnitcountName { get; set; }
    }
}
