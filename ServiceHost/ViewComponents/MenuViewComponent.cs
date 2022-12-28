using Microsoft.AspNetCore.Mvc;
using MyKalaShopQuery;
using MyKalaShopQuery.Contracts.ArticleCategory;
using MyKalaShopQuery.Contracts.ProductCategory;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;

        public MenuViewComponent(IProductCategoryQuery productCategoryQuery, 
            IArticleCategoryQuery articleCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
            _articleCategoryQuery = articleCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var model = new MenuModel()
            {
                ProductCategories = _productCategoryQuery.GetProductCategories(),
                ArticleCategories = _articleCategoryQuery.GetArticleCategories()
            };
            return View(model);
        }
    }
}
