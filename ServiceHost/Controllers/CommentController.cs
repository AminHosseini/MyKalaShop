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
            return RedirectToAction("Details", "Product", new { Slug = productSlug });
        }
    }
}
