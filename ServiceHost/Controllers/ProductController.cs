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

        [HttpGet]
        public IActionResult Details(string slug)
        {
            var model = _productQuery.GetProductBySlug(slug);
            return View(model);
        }

        [HttpGet]
        public IActionResult Search(string key)
        {
            ViewBag.SearchValue = key;
            var model = _productQuery.Search(key);
            return View(model);
        }
    }
}
