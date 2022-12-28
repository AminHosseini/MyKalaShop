using Microsoft.AspNetCore.Mvc;
using MyKalaShopQuery.Contracts.ArticleCategory;

namespace ServiceHost.Controllers
{
    public class ArticleCategoryController : Controller
    {
        private readonly IArticleCategoryQuery _articleCategoryQuery;

        public ArticleCategoryController(IArticleCategoryQuery articleCategoryQuery)
        {
            _articleCategoryQuery = articleCategoryQuery;
        }

        public IActionResult Index(string slug)
        {
            var model = _articleCategoryQuery.GetArticleCategoryBySlug(slug);
            return View(model);
        }
    }
}
