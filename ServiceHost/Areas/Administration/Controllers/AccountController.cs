using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;
        private readonly IEnumerable<IPermissionExposer> _permissionExposers;

        public AccountController(IAccountApplication accountApplication,
            IRoleApplication roleApplication, IEnumerable<IPermissionExposer> permissionExposers)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
            _permissionExposers = permissionExposers;
        }

        [HttpGet]
        public IActionResult Index(SearchAccount model)
        {
            ViewBag.Roles = GetRoles();
            var accounts = _accountApplication.Search(model);
            return View(accounts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = GetRoles();
            return PartialView("_Create", new CreateAccount());
        }

        [HttpPost]
        public JsonResult Create(CreateAccount model)
        {
            var operationResult = _accountApplication.Create(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            ViewBag.Roles = GetRoles();
            var model = _accountApplication.GetDetails(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public JsonResult Edit(EditAccount model)
        {
            var operationResult = _accountApplication.Edit(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        public IActionResult ChangePassword(long id)
        {
            return PartialView("_ChangePassword", new AccountChangePassword() { Id = id });
        }

        [HttpPost]
        public JsonResult ChangePassword(AccountChangePassword model)
        {
            var operationResult = _accountApplication.ChangePassword(model);
            return new JsonResult(operationResult);
        }

        [HttpGet]
        public IActionResult SpecifyPermissions(long id)
        {
            var model = _accountApplication.GetPermissions(id);
            ViewBag.Permissions = ExposePermissions(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult SpecifyPermissions(SpecifyAccountPermissions model)
        {
            var operationResult = _accountApplication.SpecifyPermissions(model);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index));
        }

        private List<SelectListItem> ExposePermissions(SpecifyAccountPermissions model)
        {
            var selectList = new List<SelectListItem>();

            foreach (var exposer in _permissionExposers)
            {
                var dictionary = exposer.Expose();

                foreach (var (key, value) in dictionary)
                {
                    var group = new SelectListGroup() { Name = key };

                    foreach (var permission in value)
                    {
                        var selectListItem = new SelectListItem(permission.Name, permission.Code.ToString())
                        {
                            Group = group
                        };

                        if (model.Permissions.Any(x => x == permission.Code))
                            selectListItem.Selected = true;

                        selectList.Add(selectListItem);
                    }
                }
            }

            return selectList;
        }

        private SelectList GetRoles()
        {
            return new SelectList(_roleApplication.GetRoles(), "Id", "Name");
        }
    }
}
