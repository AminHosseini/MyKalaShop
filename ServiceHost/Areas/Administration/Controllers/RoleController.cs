using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    [Authorize(Roles = Roles.Administrator)]
    public class RoleController : Controller
    {
        private readonly IRoleApplication _roleApplication;

        public RoleController(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        [HttpGet]
        [NeedsPermission(AccountPermissions.ListRoles)]
        public IActionResult Index()
        {
            var model = _roleApplication.GetRoles();
            return View(model);
        }

        [HttpGet]
        [NeedsPermission(AccountPermissions.CreateRole)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [NeedsPermission(AccountPermissions.CreateRole)]
        public IActionResult Create(CreateRole model)
        {
            var operationResult = _roleApplication.Create(model);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [NeedsPermission(AccountPermissions.EditRole)]
        public IActionResult Edit(long id)
        {
            var model = _roleApplication.GetDetails(id);
            return View(model);
        }

        [HttpPost]
        [NeedsPermission(AccountPermissions.EditRole)]
        public IActionResult Edit(EditRole model)
        {
            var operationResult = _roleApplication.Edit(model);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
