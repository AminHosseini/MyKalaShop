using Microsoft.AspNetCore.Mvc;
using MyKalaShopQuery.Contracts.ProductCategory;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;

        public ProductCategoryViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var model = _productCategoryQuery.GetProductCategories();
            return View(model);
        }
    }
}
