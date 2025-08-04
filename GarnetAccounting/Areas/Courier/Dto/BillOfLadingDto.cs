namespace GarnetAccounting.Areas.Courier.Dto
{
    public class BillOfLadingDto
    {
        public BillOfLadingDto_Header bill { get; set; }
        public List<ConsigmentDto>? Consigments { get; set; }
    }
}
