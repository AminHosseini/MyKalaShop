using Microsoft.AspNetCore.Mvc;
using MyKalaShopQuery.Contracts.Product;

namespace ShopManagement.Presentation.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductQuery _productQuery;

        public ProductApiController(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        [HttpGet]
        public List<ProductQueryView> GetProducts()
        {
            return _productQuery.GetProductsWithProductCategory();
        }
    }
}
