namespace GarnetAccounting.Areas.Courier.Dto
{
    public class BillOfLadingFilterDto
    {
        public long SellerId { get; set; }
        public string BiilOdLadingNumber { get; set; }
        public Guid? OriginBranchId { get; set; }
        public Guid? DestinationBranchId { get; set; }
        public int? RoutId { get; set; }
        public int? OriginCityId { get; set; }
        public int? DestinationCityId { get; set; }
        public string? IssuerUserName { get; set; }
        public string? strFromDate { get; set; } = DateTime.Now.LatinToPersian();
        public string? strUntilDate { get; set; }
        public short[]? BillStatus { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }
}
