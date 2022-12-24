using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    public class ArticleController : Controller
    {
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public ArticleController(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
        {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
        }

        [HttpGet]
        public IActionResult Index(SearchArticle model)
        {
            ViewBag.ArticleCategories = GetProductCategories();
            var articles = _articleApplication.Search(model);
            return View(articles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ArticleCategories = GetProductCategories();
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateArticle model)
        {
            var operationResult = _articleApplication.Create(model);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            ViewBag.ArticleCategories = GetProductCategories();
            var model = _articleApplication.GetDetails(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditArticle model)
        {
            var operationResult = _articleApplication.Edit(model);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index));
        }

        private SelectList GetProductCategories()
        {
            return new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
        }
    }
}
