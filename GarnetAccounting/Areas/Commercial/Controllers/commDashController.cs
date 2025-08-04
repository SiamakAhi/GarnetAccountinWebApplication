using GarnetAccounting.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.Commercial.Controllers
{
    [Area("Commercial")]
    [Authorize]
    public class commDashController : Controller
    {
        private readonly UserContextService _userContext;
        public commDashController(UserContextService userContextService)
        {
            _userContext = userContextService;
        }
        public IActionResult dashboard()
        {
            if (_userContext == null) return NoContent();

            return View();
        }
    }
}
