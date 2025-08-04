namespace GarnetAccounting.Areas.Accounting.Dto.PrintDto
{
    public class StructuredKolGroupDto
    {
        public int KolId { get; set; }
        public string KolCode { get; set; }
        public string KolName { get; set; }
        public long TotalBed { get; set; }
        public long TotalBes { get; set; }
        public List<StructuredMoeinGroupDto> MoeinGroups { get; set; } = new List<StructuredMoeinGroupDto>();
    }

}
