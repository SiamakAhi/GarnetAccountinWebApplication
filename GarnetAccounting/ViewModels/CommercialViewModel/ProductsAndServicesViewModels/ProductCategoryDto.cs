namespace GarnetAccounting.ViewModels.CommercialViewModel.ProductsAndServicesViewModels
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public int? customerId { get; set; }
        public Int64? SellerId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }
    }
}
