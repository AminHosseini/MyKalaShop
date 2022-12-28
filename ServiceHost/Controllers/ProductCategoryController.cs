using Microsoft.AspNetCore.Mvc;
using MyKalaShopQuery.Contracts.ProductCategory;

namespace ServiceHost.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryQuery _productCategoryQuery;

        public ProductCategoryController(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public IActionResult Index(string slug)
        {
            var model = _productCategoryQuery.GetProductCategoriesWithProductsBySlug(slug);
            return View(model);
        }
    }
}
