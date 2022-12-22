using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    public class RoleController : Controller
    {
        private readonly IRoleApplication _roleApplication;

        public RoleController(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _roleApplication.GetRoles();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateRole model)
        {
            var operationResult = _roleApplication.Create(model);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var model = _roleApplication.GetDetails(id);
            return View(model);
        }

        [HttpPost]
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
