namespace GarnetAccounting.Areas.Warehouse.Dto
{
    public class ProductFilter
    {
        public long SellerId { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public List<long>? CategoryIds { get; set; } = new List<long>();
    }
}
