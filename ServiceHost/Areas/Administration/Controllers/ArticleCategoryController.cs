using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    [Authorize(Roles = Roles.Administrator)]
    public class ArticleCategoryController : Controller
    {
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public ArticleCategoryController(IArticleCategoryApplication articleCategoryApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
        }

        [HttpGet]
        [NeedsPermission(BlogPermissions.ListArticleCategories)]
        public IActionResult Index(SearchArticleCategory model)
        {
            var articleCategories = _articleCategoryApplication.Search(model);
            return View(articleCategories);
        }

        [HttpGet]
        [NeedsPermission(BlogPermissions.CreateArticleCategory)]
        public IActionResult Create()
        {
            return PartialView("_Create", new CreateArticleCategory());
        }

        [HttpPost]
        [NeedsPermission(BlogPermissions.CreateArticleCategory)]
        public JsonResult Create(CreateArticleCategory model)
        {
            var operationResult = _articleCategoryApplication.Create(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        [NeedsPermission(BlogPermissions.EditArticleCategory)]
        public IActionResult Edit(long id)
        {
            var model = _articleCategoryApplication.GetDetails(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        [NeedsPermission(BlogPermissions.EditArticleCategory)]
        public JsonResult Edit(EditArticleCategory model)
        {
            var operationResult = _articleCategoryApplication.Edit(model);
            return new JsonResult(operationResult);
        }
    }
}
