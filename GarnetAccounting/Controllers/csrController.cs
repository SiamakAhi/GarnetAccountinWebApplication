using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using GarnetAccounting.Areas.AvaRasta.Interfaces;
using GarnetAccounting.Areas.AvaRasta.ViewModels;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Interfaces.Identity;
using GarnetAccounting.ViewModels.Tax;

namespace GarnetAccounting.Controllers
{

    [Authorize]
    public class csrController : Controller
    {
        protected readonly ICsrService _csr;
        private readonly IAppIdentityUserManager _userManager;
        private readonly IGeneralService _gs;
        private readonly ICostomerService _customerService;
        public csrController(ICsrService csr
            , IAppIdentityUserManager userManager
            , IGeneralService generalService
            , ICostomerService costomerService)
        {
            _csr = csr;
            _userManager = userManager;
            _gs = generalService;
            _customerService = costomerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCSRHagh()
        {
            int? customerId = await _userManager.GetCustomerIdByUsername(User.Identity.Name);
            VmCustomer? cusInfo = await _customerService.GetVmCustomerByIdAsync(customerId.Value);
            VmCSR model = new VmCSR();
            string? fullname = cusInfo?.FName + cusInfo?.LName;

            model.Haghighi = new CsrInfoHaghighi()
            {
                Email = cusInfo?.Email,
                CommonName = fullname?.ToFinglish(),
                SerialNumber = cusInfo?.NationalId,
                GivenName = cusInfo?.FName,
                Surname = cusInfo?.LName,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetCSRHagh(VmCSR model)
        {
            if (ModelState.IsValid)
            {
                var result = _csr.GenerateCsrForHaghighi(model.Haghighi);
                model.CsrResult = result;
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetCSRCompany()
        {
            int? customerId = await _userManager.GetCustomerIdByUsername(User.Identity.Name);
            VmCustomer? cusInfo = await _customerService.GetVmCustomerByIdAsync(customerId.Value);
            VmCSR model = new VmCSR();
            model.Hoghooghi = new CsrInfoHoghooghi()
            {
                Email = cusInfo.Email,
                OrganizationalUnit1 = cusInfo.Title,
                CommonName = cusInfo.Title.ToFinglish(),
                SerialNumber = cusInfo.NationalId,
            };


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetCSRCompany(VmCSR model)
        {
            if (ModelState.IsValid)
            {
                var result = _csr.GenerateCsrForHoghooghi(model.Hoghooghi);
                model.CsrResult = result;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult DownloadText(string keytext, string fileName)
        {
            var byteArray = Encoding.UTF8.GetBytes(keytext);
            var stream = new MemoryStream(byteArray);
            string FileName = fileName + ".txt";
            return File(stream, "text/plain", FileName);
        }


        [HttpPost]
        public ActionResult DownloadCrt(string keytext, string fileName)
        {
            var byteArray = Encoding.UTF8.GetBytes(keytext);
            var stream = new MemoryStream(byteArray);
            string FileName = fileName + ".crt";
            return File(stream, "application/x-pem-file", FileName);
        }
        [HttpPost]
        public ActionResult DownloadPem(string keytext, string fileName)
        {
            var byteArray = Encoding.UTF8.GetBytes(keytext);
            var stream = new MemoryStream(byteArray);
            string FileName = fileName + ".pem";
            return File(stream, "application/x-pem-file", FileName);
        }
    }
}
