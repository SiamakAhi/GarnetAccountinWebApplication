namespace GarnetAccounting.Areas.Accounting.Dto.PrintDto
{
    public class StructuredMoeinGroupDto
    {
        public int KolId { get; set; }
        public int MoeinId { get; set; }
        public string MoeinCode { get; set; }
        public string MoeinName { get; set; }
        public long TotalBed { get; set; }
        public long TotalBes { get; set; }
        public List<StructuredTafsilDto> TafsilDetails { get; set; } = new List<StructuredTafsilDto>();
    }

}
