namespace GarnetAccounting.Areas.CustomerArea.Dto
{
    public class UserSellerDto
    {
        public Int64 Id { get; set; }
        public Int64 SellerId { get; set; }
        public int? CustomerId { get; set; }
        public string? SellerName { get; set; }
        public string UserName { get; set; }
    }
}
