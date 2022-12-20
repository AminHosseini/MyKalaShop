using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryApplication _productCategoryApplication;

        public ProductCategoryController(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        [HttpGet]
        public IActionResult Index(SearchProductCategory searchModel)
        {
            var model = _productCategoryApplication.Search(searchModel);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_Create", new CreateProductCategory());
        }

        [HttpPost]
        public JsonResult Create(CreateProductCategory model)
        {
            var operationResult = _productCategoryApplication.Create(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var model = _productCategoryApplication.GetDetails(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public JsonResult Edit(EditProductCategory model)
        {
            var operationResult = _productCategoryApplication.Edit(model);
            return new JsonResult(operationResult);
        }
    }
}
