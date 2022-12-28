using CommentManagement.Application.Contracts.Comment;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    public class CommentController : Controller
    {
        private readonly ICommentApplication _commentApplication;

        public CommentController(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }

        [HttpGet]
        public IActionResult Index(SearchComment model)
        {
            var comments = _commentApplication.Search(model);
            return View(comments);
        }

        [HttpGet]
        public IActionResult Confirm(long id)
        {
            var operationResult = _commentApplication.Confirm(id);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Cancel(long id)
        {
            var operationResult = _commentApplication.Cancel(id);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
