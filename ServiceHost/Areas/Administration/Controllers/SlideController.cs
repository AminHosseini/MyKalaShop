using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using _0_Framework.Infrastructure;
using ShopManagement.Infrastructure.Configuration.Permissions;
using ShopManagement.Application.Contracts.Slide;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    [Authorize(Roles = Roles.Administrator)]
    public class SlideController : Controller
    {
        private readonly ISlideApplication _slideApplication;

        public SlideController(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }

        [HttpGet]
        [NeedsPermission(ShopPermissions.ListSlides)]
        public IActionResult Index()
        {
            var model = _slideApplication.GetSlides();
            return View(model);
        }

        [HttpGet]
        [NeedsPermission(ShopPermissions.CreateSlide)]
        public IActionResult Create()
        {
            return PartialView("_Create", new CreateSlide());
        }

        [HttpPost]
        [NeedsPermission(ShopPermissions.CreateSlide)]
        public JsonResult Create(CreateSlide model)
        {
            var operationResult = _slideApplication.Create(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        [NeedsPermission(ShopPermissions.EditSlide)]
        public IActionResult Edit(long id)
        {
            var model = _slideApplication.GetDetails(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        [NeedsPermission(ShopPermissions.EditSlide)]
        public JsonResult Edit(EditSlide model)
        {
            var operationResult = _slideApplication.Edit(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        [NeedsPermission(ShopPermissions.DeleteSlide)]
        public IActionResult Delete(long id)
        {
            var operationResult = _slideApplication.Delete(id);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [NeedsPermission(ShopPermissions.RestoreSlide)]
        public IActionResult Restore(long id)
        {
            var operationResult = _slideApplication.Restore(id);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
