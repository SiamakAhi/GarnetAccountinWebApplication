using GarnetAccounting.Areas.Organization.OrgInterfaces;
using GarnetAccounting.Models;

namespace GarnetAccounting.Areas.Organization.OrgServices
{
    public class OrganizationService : IOrganizationService
    {
        private readonly AppDbContext _db;
        public OrganizationService(AppDbContext db)
        {
            _db = db;
        }
    }
}
