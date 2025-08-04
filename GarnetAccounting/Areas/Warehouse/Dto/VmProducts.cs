namespace GarnetAccounting.Areas.Warehouse.Dto
{
    public class VmProducts
    {
        public ProductFilter? filter { get; set; }
        public Pagination<ProductBaseDto> Products { get; set; }
    }
}
