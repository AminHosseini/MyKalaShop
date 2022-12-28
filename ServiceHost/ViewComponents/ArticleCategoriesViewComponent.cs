using Microsoft.AspNetCore.Mvc;
using MyKalaShopQuery.Contracts.ArticleCategory;

namespace ServiceHost.ViewComponents
{
    public class ArticleCategoriesViewComponent : ViewComponent
    {
        private readonly IArticleCategoryQuery _articleCategoryQuery;

        public ArticleCategoriesViewComponent(IArticleCategoryQuery articleCategoryQuery)
        {
            _articleCategoryQuery = articleCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var model = _articleCategoryQuery.GetArticleCategories();
            return View(model);
        }
    }
}
