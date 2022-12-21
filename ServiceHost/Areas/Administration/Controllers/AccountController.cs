using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Route("Administration/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountApplication _accountApplication;

        public AccountController(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        [HttpGet]
        public IActionResult Index(SearchAccount model)
        {
            //ViewBag.Roles = new SelectList();
            var accounts = _accountApplication.Search(model);
            return View(accounts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewBag.Roles = new SelectList();
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
            //ViewBag.Roles = new SelectList();
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
    }
}
