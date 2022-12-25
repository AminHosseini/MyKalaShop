using Microsoft.AspNetCore.Mvc;
using MyKalaShopQuery.Contracts.Product;
using MyKalaShopQuery.Contracts.ProductCategory;

namespace ServiceHost.ViewComponents
{
    public class ProductsWithProductCategoryViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public ProductsWithProductCategoryViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public IViewComponentResult Invoke()
        {
            var model = _productQuery.GetProductsWithProductCategory();
            return View(model);
        }
    }
}
