using _0_Framework.Infrastructure;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    [Authorize(Roles = Roles.Administrator)]
    public class CommentController : Controller
    {
        private readonly ICommentApplication _commentApplication;

        public CommentController(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }

        [HttpGet]
        [NeedsPermission(CommentPermissions.ListComments)]
        public IActionResult Index(SearchComment model)
        {
            var comments = _commentApplication.Search(model);
            return View(comments);
        }

        [HttpGet]
        [NeedsPermission(CommentPermissions.ConfirmComment)]
        public IActionResult Confirm(long id)
        {
            var operationResult = _commentApplication.Confirm(id);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [NeedsPermission(CommentPermissions.CancelComment)]
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
