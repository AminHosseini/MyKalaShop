using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    public class ArticleCategoryController : Controller
    {
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public ArticleCategoryController(IArticleCategoryApplication articleCategoryApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
        }

        [HttpGet]
        public IActionResult Index(SearchArticleCategory model)
        {
            var articleCategories = _articleCategoryApplication.Search(model);
            return View(articleCategories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_Create", new CreateArticleCategory());
        }

        [HttpPost]
        public JsonResult Create(CreateArticleCategory model)
        {
            var operationResult = _articleCategoryApplication.Create(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var model = _articleCategoryApplication.GetDetails(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public JsonResult Edit(EditArticleCategory model)
        {
            var operationResult = _articleCategoryApplication.Edit(model);
            return new JsonResult(operationResult);
        }
    }
}
