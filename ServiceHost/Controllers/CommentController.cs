using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentApplication _commentApplication;

        public CommentController(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }

        [HttpPost]
        public IActionResult CreateProductComment(CreateComment model, string productSlug)
        {
            model.CommentType = CommentType.ProductComment;
            var operationResult = _commentApplication.Create(model);

            if (operationResult.IsSuccess)
                return RedirectToAction("Details", "Product", new { Slug = productSlug });

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction("Details", "Product", new { Slug = productSlug });
        }

        [HttpPost]
        public IActionResult CreateArticleComment(CreateComment model, string articleSlug)
        {
            model.CommentType = CommentType.ArticleComment;
            var operationResult = _commentApplication.Create(model);

            if (operationResult.IsSuccess)
                return RedirectToAction("Details", "Article", new { Slug = articleSlug });

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction("Details", "Article", new { Slug = articleSlug });
        }
    }
}
