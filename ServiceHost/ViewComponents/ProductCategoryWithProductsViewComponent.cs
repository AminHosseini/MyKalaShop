using Microsoft.AspNetCore.Mvc;
using MyKalaShopQuery.Contracts.ProductCategory;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryWithProductsViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;

        public ProductCategoryWithProductsViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var model = _productCategoryQuery.GetProductCategoryWithProducts();
            return View(model);
        }
    }
}
