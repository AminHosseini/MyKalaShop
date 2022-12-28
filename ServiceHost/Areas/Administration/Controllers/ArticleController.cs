using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    [Authorize(Roles = Roles.Administrator)]
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
        [NeedsPermission(BlogPermissions.ListArticles)]
        public IActionResult Index(SearchArticle model)
        {
            ViewBag.ArticleCategories = GetProductCategories();
            var articles = _articleApplication.Search(model);
            return View(articles);
        }

        [HttpGet]
        [NeedsPermission(BlogPermissions.CreateArticle)]
        public IActionResult Create()
        {
            ViewBag.ArticleCategories = GetProductCategories();
            return View();
        }

        [HttpPost]
        [NeedsPermission(BlogPermissions.CreateArticle)]
        public IActionResult Create(CreateArticle model)
        {
            var operationResult = _articleApplication.Create(model);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [NeedsPermission(BlogPermissions.EditArticle)]
        public IActionResult Edit(long id)
        {
            ViewBag.ArticleCategories = GetProductCategories();
            var model = _articleApplication.GetDetails(id);
            return View(model);
        }

        [HttpPost]
        [NeedsPermission(BlogPermissions.EditArticle)]
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
