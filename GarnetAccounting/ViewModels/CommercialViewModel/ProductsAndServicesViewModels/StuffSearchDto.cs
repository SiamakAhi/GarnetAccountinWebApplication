namespace GarnetAccounting.ViewModels.CommercialViewModel.ProductsAndServicesViewModels
{
    public class StuffSearchDto
    {
        public Pagination<XmlSearchResultDto>? ResultList { get; set; }
        public StuffFilterDto? Filter { get; set; }
    }
}
