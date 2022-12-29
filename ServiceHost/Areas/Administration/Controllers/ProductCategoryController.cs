using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using _0_Framework.Infrastructure;
using ShopManagement.Infrastructure.Configuration.Permissions;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    [Authorize(Roles = Roles.Administrator)]
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryApplication _productCategoryApplication;

        public ProductCategoryController(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        [HttpGet]
        [NeedsPermission(ShopPermissions.ListProductCategories)]
        public IActionResult Index(SearchProductCategory searchModel)
        {
            var model = _productCategoryApplication.Search(searchModel);
            return View(model);
        }

        [HttpGet]
        [NeedsPermission(ShopPermissions.CreateProductCategory)]
        public IActionResult Create()
        {
            return PartialView("_Create", new CreateProductCategory());
        }

        [HttpPost]
        [NeedsPermission(ShopPermissions.CreateProductCategory)]
        public JsonResult Create(CreateProductCategory model)
        {
            var operationResult = _productCategoryApplication.Create(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        [NeedsPermission(ShopPermissions.EditProductCategory)]
        public IActionResult Edit(long id)
        {
            var model = _productCategoryApplication.GetDetails(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        [NeedsPermission(ShopPermissions.EditProductCategory)]
        public JsonResult Edit(EditProductCategory model)
        {
            var operationResult = _productCategoryApplication.Edit(model);
            return new JsonResult(operationResult);
        }
    }
}
