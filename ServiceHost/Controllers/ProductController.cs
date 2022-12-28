using Microsoft.AspNetCore.Mvc;
using MyKalaShopQuery.Contracts.Product;

namespace ServiceHost.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductQuery _productQuery;

        public ProductController(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public IActionResult Details(string slug)
        {
            var model = _productQuery.GetProductBySlug(slug);
            return View(model);
        }
    }
}
