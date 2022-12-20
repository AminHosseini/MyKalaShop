using DiscountManagement.Application.Contracts.ColleagueDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using InventoryManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    public class ColleagueDiscountController : Controller
    {
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;
        private readonly IProductApplication _productApplication;

        public ColleagueDiscountController(IColleagueDiscountApplication colleagueDiscountApplication, IProductApplication productApplication)
        {
            _colleagueDiscountApplication = colleagueDiscountApplication;
            _productApplication = productApplication;
        }

        [HttpGet]
        public IActionResult Index(SearchColleagueDiscount model)
        {
            ViewBag.Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            var colleagueDiscounts = _colleagueDiscountApplication.Search(model);
            return View(colleagueDiscounts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            return PartialView("_Create", new CreateColleagueDiscount());
        }

        [HttpPost]
        public JsonResult Create(CreateColleagueDiscount model)
        {
            var operationResult = _colleagueDiscountApplication.Create(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            ViewBag.Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            var model = _colleagueDiscountApplication.GetDetails(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public JsonResult Edit(EditColleagueDiscount model)
        {
            var operationResult = _colleagueDiscountApplication.Edit(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            var operationResult = _colleagueDiscountApplication.Delete(id);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Restore(long id)
        {
            var operationResult = _colleagueDiscountApplication.Restore(id);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
