using Microsoft.AspNetCore.Mvc;
using MyKalaShopQuery.Contracts.Article;

namespace ServiceHost.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleQuery _articleQuery;

        public ArticleController(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }

        public IActionResult Details(string slug)
        {
            var model = _articleQuery.GetArticleBySlug(slug);
            return View(model);
        }
    }
}
