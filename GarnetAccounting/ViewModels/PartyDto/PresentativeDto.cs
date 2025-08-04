namespace GarnetAccounting.ViewModels.PartyDto
{
    public class PresentativeDto
    {
        public long RepresentativeId { get; set; }
        public long ParentId { get; set; }
        public string? Name { get; set; }
        public string? IdentityNumber { get; set; }
        public string? ParentName { get; set; }
        public string? phone { get; set; }

    }
}
