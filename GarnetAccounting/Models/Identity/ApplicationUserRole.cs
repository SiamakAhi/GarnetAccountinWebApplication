using Microsoft.AspNetCore.Identity;

namespace GarnetAccounting.Models.Identity
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual AppRole Role { get; set; }
        public virtual AppIdentityUser User { get; set; }
    }
}
