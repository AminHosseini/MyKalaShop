using DiscountManagement.Application.Contracts.ColleagueDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using _0_Framework.Infrastructure;
using DiscountManagement.Infrastructure.Configuration.Permissions;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    [Authorize(Roles = Roles.Administrator)]
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
        [NeedsPermission(DiscountPermissions.ListColleagueDiscounts)]
        public IActionResult Index(SearchColleagueDiscount model)
        {
            ViewBag.Products = GetProducts();
            var colleagueDiscounts = _colleagueDiscountApplication.Search(model);
            return View(colleagueDiscounts);
        }

        [HttpGet]
        [NeedsPermission(DiscountPermissions.CreateColleagueDiscount)]
        public IActionResult Create()
        {
            ViewBag.Products = GetProducts();
            return PartialView("_Create", new CreateColleagueDiscount());
        }

        [HttpPost]
        [NeedsPermission(DiscountPermissions.CreateColleagueDiscount)]
        public JsonResult Create(CreateColleagueDiscount model)
        {
            var operationResult = _colleagueDiscountApplication.Create(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        [NeedsPermission(DiscountPermissions.EditColleagueDiscount)]
        public IActionResult Edit(long id)
        {
            ViewBag.Products = GetProducts();
            var model = _colleagueDiscountApplication.GetDetails(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        [NeedsPermission(DiscountPermissions.EditColleagueDiscount)]
        public JsonResult Edit(EditColleagueDiscount model)
        {
            var operationResult = _colleagueDiscountApplication.Edit(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        [NeedsPermission(DiscountPermissions.DeleteColleagueDiscount)]
        public IActionResult Delete(long id)
        {
            var operationResult = _colleagueDiscountApplication.Delete(id);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [NeedsPermission(DiscountPermissions.RestoreColleagueDiscount)]
        public IActionResult Restore(long id)
        {
            var operationResult = _colleagueDiscountApplication.Restore(id);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index));
        }

        private SelectList GetProducts()
        {
            return new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }
    }
}
