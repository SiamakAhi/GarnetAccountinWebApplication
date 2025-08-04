using Microsoft.AspNetCore.Identity;

namespace GarnetAccounting.Models.Identity
{
    public class AppRole : IdentityRole
    {
        public AppRole()
        {

        }
        public AppRole(string Name) : base(Name)
        {

        }
        public AppRole(string Name, string description) : base(Name)
        {
            Description = description;
        }

        public string Description { get; set; }
        public virtual List<AppIdentityUser> Users { get; set; }
    }
}
