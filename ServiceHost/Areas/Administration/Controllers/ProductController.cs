using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using _0_Framework.Infrastructure;
using ShopManagement.Infrastructure.Configuration.Permissions;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    [Authorize(Roles = Roles.Administrator)]
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
        [NeedsPermission(ShopPermissions.ListProducts)]
        public IActionResult Index(SearchProduct searchModel)
        {
            ViewBag.ProductCategories = GetProductCategories();
            var model = _productApplication.Search(searchModel);
            return View(model);
        }

        [HttpGet]
        [NeedsPermission(ShopPermissions.CreateProduct)]
        public IActionResult Create()
        {
            ViewBag.ProductCategories = GetProductCategories();
            return PartialView("_Create", new CreateProduct());
        }

        [HttpPost]
        [NeedsPermission(ShopPermissions.CreateProduct)]
        public JsonResult Create(CreateProduct model)
        {
            var operationResult = _productApplication.Create(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        [NeedsPermission(ShopPermissions.EditProduct)]
        public IActionResult Edit(long id)
        {
            ViewBag.ProductCategories = GetProductCategories();
            var model = _productApplication.GetDetails(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        [NeedsPermission(ShopPermissions.EditProduct)]
        public JsonResult Edit(EditProduct model)
        {
            var operationResult = _productApplication.Edit(model);
            return new JsonResult(operationResult);
        }

        private SelectList GetProductCategories()
        {
            return new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name");
        }
    }
}
