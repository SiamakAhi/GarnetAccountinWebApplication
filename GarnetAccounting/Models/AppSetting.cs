namespace GarnetAccounting.Models
{
    public class AppSettings
    {
        public int Id { get; set; }
        public string? OwnerName { get; set; }
        public string? CompanyName { get; set; }
        public string? AppName { get; set; }
        public string? LogoUrl { get; set; }
        public string? LoginMessage { get; set; }
        public DateTime? LunchDate { get; set; }
        public DateTime? ExpierDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string? Version { get; set; }

    }
}
