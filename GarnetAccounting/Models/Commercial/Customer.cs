using GarnetAccounting.Areas.AvaRasta.Models.Entities;
using GarnetAccounting.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace GarnetAccounting.Models.Commercial
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public string FName { get; set; }
        public string LName { get; set; }
        public string Title { get; set; }
        public bool Ishoghooghi { get; set; } = true;
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string? Fax { get; set; }
        public string? EconomicNumber { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? NationalId { get; set; }
        public int? State { get; set; }
        public int? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public int LicenseCount { get; set; } = 1;
        public int InvoiceCountLimit { get; set; } = 0;
        public DateTime RegisterDate { get; set; }
        public DateTime? ActivationDate { get; set; }
        public DateTime? licenseExpierDate { get; set; }

        public string? VersionType { get; set; }
        public string? LogoAddress { get; set; }
        public long? TafsilId { get; set; }
        public string? TafsilCode { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string UserCreator { get; set; }
        public bool IsActive { get; set; }
        public string? Username { get; set; }
        public virtual ICollection<AppIdentityUser> CustomerUsers { get; set; }
        public virtual ICollection<Party> CustomerParties { get; set; }
        public virtual ICollection<License> Licenses { get; set; }
    }
}
