using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using _0_Framework.Infrastructure;
using ShopManagement.Infrastructure.Configuration.Permissions;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    [Authorize(Roles = Roles.Administrator)]
    public class ProductPictureController : Controller
    {
        private readonly IProductPictureApplication _productPictureApplication;
        private readonly IProductApplication _productApplication;

        public ProductPictureController(IProductPictureApplication productPictureApplication,
            IProductApplication productApplication)
        {
            _productPictureApplication = productPictureApplication;
            _productApplication = productApplication;
        }

        [HttpGet]
        [NeedsPermission(ShopPermissions.ListProductPictures)]
        public IActionResult Index(SearchProductPicture model)
        {
            ViewBag.Products = GetProducts();
            var productPictures = _productPictureApplication.Search(model);
            return View(productPictures);
        }

        [HttpGet]
        [NeedsPermission(ShopPermissions.CreateProductPicture)]
        public IActionResult Create()
        {
            ViewBag.Products = GetProducts();
            return PartialView("_Create", new CreateProductPicture());
        }

        [HttpPost]
        [NeedsPermission(ShopPermissions.CreateProductPicture)]
        public JsonResult Create(CreateProductPicture model)
        {
            var operationResult = _productPictureApplication.Create(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        [NeedsPermission(ShopPermissions.EditProductPicture)]
        public IActionResult Edit(long id)
        {
            ViewBag.Products = GetProducts();
            var productPicture = _productPictureApplication.GetDetails(id);
            return PartialView("_Edit", productPicture);
        }

        [HttpPost]
        [NeedsPermission(ShopPermissions.EditProductPicture)]
        public JsonResult Edit(EditProductPicture model)
        {
            var operationResult = _productPictureApplication.Edit(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        [NeedsPermission(ShopPermissions.DeleteProductPicture)]
        public IActionResult Delete(long id)
        {
            var operationResult = _productPictureApplication.Delete(id);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [NeedsPermission(ShopPermissions.RestoreProductPicture)]
        public IActionResult Restore(long id)
        {
            var operationResult = _productPictureApplication.Restore(id);

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
