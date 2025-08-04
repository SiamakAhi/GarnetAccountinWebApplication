using GarnetAccounting.Areas.Accounting.AccountingInterfaces;
using GarnetAccounting.Areas.Accounting.Dto;
using GarnetAccounting.Areas.Projects.ProjectInterfaces;
using GarnetAccounting.Interfaces;
using GarnetAccounting.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GarnetAccounting.Areas.Projects.Controllers
{
    [Area("Projects")]
    [Authorize]
    public class ConProjectController : Controller
    {
        private readonly IConProjectService _projectService;
        private readonly UserContextService _userContext;
        private readonly IAccCodingService _acccoding;
        private readonly IPersonService _person;

        public ConProjectController(IConProjectService projectService
            , UserContextService userContext
            , IAccCodingService AccountingCodingService
            , IPersonService personService)
        {
            _projectService = projectService;
            _userContext = userContext;
            _acccoding = AccountingCodingService;
            _person = personService;
        }

        public async Task<IActionResult> Index()
        {
            if (!_userContext.SellerId.HasValue) return NotFound();

            var projects = await _projectService.GetProjectsAsync(_userContext.SellerId.Value);
            return View(projects);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (!_userContext.SellerId.HasValue) return NotFound();

            ViewBag.tafsil = await _acccoding.SelectList_TafsilsAsync(_userContext.SellerId.Value);
            ViewBag.clients = await _person.SelectList_PersenAsync(_userContext.SellerId.Value);

            return PartialView("_CreateProject");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConProjectDto dto)
        {
            var result = new clsResult();
            result.Success = false;
            if (!_userContext.SellerId.HasValue)
            {
                result.Message = "شرکت فعال یافت نشد";
                return Json(result.ToJsonResult());
            }

            dto.SellerId = _userContext.SellerId.Value;
            if (!string.IsNullOrEmpty(dto.strDate))
                dto.ProjectStartDate = dto.strDate.PersianToLatin();
            if (ModelState.IsValid)
            {

                result = await _projectService.CreateProjectAsync(dto);
                if (result.Success)
                {
                    result.ShowMessage = true;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.updateType = 1;
                }
                return Json(result.ToJsonResult());
            }

            result.Message = string.Join("\n", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
            result.ShowMessage = true;

            return Json(result.ToJsonResult());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            ViewBag.tafsil = await _acccoding.SelectList_TafsilsAsync(_userContext.SellerId.Value);
            ViewBag.clients = await _person.SelectList_PersenAsync(_userContext.SellerId.Value);

            return PartialView("_EditProject", project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ConProjectDto dto)
        {
            var result = new clsResult();
            result.Success = false;
            if (!_userContext.SellerId.HasValue)
            {
                result.Message = "شرکت فعال یافت نشد";
                return Json(result.ToJsonResult());
            }

            dto.SellerId = _userContext.SellerId.Value;
            if (!string.IsNullOrEmpty(dto.strDate))
                dto.ProjectStartDate = dto.strDate.PersianToLatin();
            if (ModelState.IsValid)
            {
                result = await _projectService.UpdateProjectAsync(dto);
                if (result.Success)
                {
                    result.ShowMessage = true;
                    result.returnUrl = Request.Headers["Referer"].ToString();
                    result.updateType = 1;
                }
                return Json(result.ToJsonResult());
            }

            result.Message = string.Join("\n", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
            result.ShowMessage = true;
            return Json(result.ToJsonResult());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int itemId)
        {
            var result = await _projectService.DeleteProjectAsync(itemId);
            if (result.Success)
            {
                result.returnUrl = Request.Headers["Referer"].ToString();
                result.ShowMessage = true;
                result.updateType = 1;
            }
            return Json(result.ToJsonResult());
        }
    }
}