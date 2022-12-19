using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
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
        public IActionResult Index(SearchProductPicture model)
        {
            ViewBag.Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            var productPictures = _productPictureApplication.Search(model);
            return View(productPictures);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            return PartialView("_Create", new CreateProductPicture());
        }

        [HttpPost]
        public JsonResult Create(CreateProductPicture model)
        {
            var operationResult = _productPictureApplication.Create(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            ViewBag.Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            var productPicture = _productPictureApplication.GetDetails(id);
            return PartialView("_Edit", productPicture);
        }

        [HttpPost]
        public JsonResult Edit(EditProductPicture model)
        {
            var operationResult = _productPictureApplication.Edit(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            var operationResult = _productPictureApplication.Delete(id);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Restore(long id)
        {
            var operationResult = _productPictureApplication.Restore(id);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
