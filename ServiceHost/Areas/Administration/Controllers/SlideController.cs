using Microsoft.AspNetCore.Mvc;
using ShopManagement.Application.Contracts.Slide;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    public class SlideController : Controller
    {
        private readonly ISlideApplication _slideApplication;

        public SlideController(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _slideApplication.GetSlides();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_Create", new CreateSlide());
        }

        [HttpPost]
        public JsonResult Create(CreateSlide model)
        {
            var operationResult = _slideApplication.Create(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var model = _slideApplication.GetDetails(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public JsonResult Edit(EditSlide model)
        {
            var operationResult = _slideApplication.Edit(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            var operationResult = _slideApplication.Delete(id);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
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
