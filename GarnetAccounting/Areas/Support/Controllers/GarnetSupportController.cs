using Microsoft.AspNetCore.Mvc;
using GarnetAccounting.Areas.Support.Dtos;
using GarnetAccounting.Areas.Support.SuportInterfaces;
using GarnetAccounting.Classes;

namespace GarnetAccounting.Areas.Support.Controllers
{
    [Area("Support")]
    public class GarnetSupportController : Controller
    {
        private readonly ISupportService _support;

        public GarnetSupportController(ISupportService support)
        {
            _support = support;
        }

        [HttpGet]
        public IActionResult SendSmsSupport()
        {
            return PartialView("_SendSmsSupport");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendSmsSupport(UserSendSmsDto dto)
        {
            clsResult result = new clsResult();
            result.Success = false;
            if (ModelState.IsValid)
            {
                var UserInfo = await _support.GetUserInfoAsync(User.Identity.Name);
                dto.SenderName = UserInfo.UserFullName;
                dto.SenderCustomerName = UserInfo.CustomerName;
                dto.Date = DateTime.Now.Date;
                dto.Time = new TimeOnly(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                dto.SenderUserId = User.Identity.Name;
                //var res = await _support.SendSmsToSupportAsync(dto.Message);
                // if (res.Status == 200)
                string emailBody = EmailSender.CreateUserMessageForSupport(dto);

                EmailSender.Send("Ahi.siamak@gmail.com", dto.Subject, emailBody);
                EmailSender.Send("af.hooman@yahoo.com", dto.Subject, emailBody);
                result.Success = true;
                result.Message = "پیام شما با موفقیت برای پشتیبانی ارسال شد.";
                // result.returnUrl = Request.Headers["Referer"].ToString();
            }

            return Json(result.ToJsonResult());
        }
    }
}
