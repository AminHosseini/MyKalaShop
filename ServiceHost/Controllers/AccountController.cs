using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountApplication _accountApplication;

        public AccountController(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(CreateAccount model)
        {
            model.RoleId = int.Parse(Roles.SystemUser);
            var operationResult = _accountApplication.Create(model);

            if (operationResult.IsSuccess)
            {
                ViewBag.Message = ValidationMessage.SuccessfullyRegistered;
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = operationResult.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var operationResult = _accountApplication.Login(model);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index), "Home");

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            _accountApplication.Logout();
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
