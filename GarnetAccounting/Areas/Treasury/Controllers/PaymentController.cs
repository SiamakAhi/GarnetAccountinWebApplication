using GarnetAccounting.Areas.Treasury.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.Treasury.Controllers
{
    [Area("Treasury")]
    public class PaymentController : Controller
    {



        public IActionResult RequestPayment([FromBody] PaymentRequestDto request)
        {
            if (request.Amount <= 0 || string.IsNullOrEmpty(request.OrderId))
            {
                return BadRequest("Invalid payment request.");
            }

            return View();
        }


        public IActionResult PaymentCallback(PaymentCallbackDto callbackData)
        {
            return View();
        }
        public IActionResult PaymentCallback()
        {
            return View();
        }
    }
}
