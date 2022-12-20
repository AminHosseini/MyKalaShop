using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using InventoryManagement.Application.Contracts.Product;
using InventoryManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    public class ProductController : Controller
    {
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public ProductController(IProductApplication productApplication, 
            IProductCategoryApplication productCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }

        [HttpGet]
        public IActionResult Index(SearchProduct searchModel)
        {
            ViewBag.ProductCategories =
                new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name");
            var model = _productApplication.Search(searchModel);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ProductCategories = 
                new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name");
            return PartialView("_Create", new CreateProduct());
        }

        [HttpPost]
        public JsonResult Create(CreateProduct model)
        {
            var operationResult = _productApplication.Create(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            ViewBag.ProductCategories = 
                new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name");
            var model = _productApplication.GetDetails(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public JsonResult Edit(EditProduct model)
        {
            var operationResult = _productApplication.Edit(model);
            return new JsonResult(operationResult);
        }
    }
}
