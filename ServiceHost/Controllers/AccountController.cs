using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountApplication _accountApplication;
        private readonly IAccountService _accountService;

        public AccountController(IAccountApplication accountApplication,
            IAccountService accountService)
        {
            _accountApplication = accountApplication;
            _accountService = accountService;
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
        public IActionResult Login(Login model)
        {
            var operationResult = _accountApplication.Login(model);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index), "Home");

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index), "Account");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            _accountApplication.Logout();
            return RedirectToAction(nameof(Index), "Home");
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        public IActionResult Edit()
        {
            var model = _accountApplication.GetLoggedInUserDetails();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditAccount model)
        {
            var operationResult = _accountApplication.Edit(model);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index), "Home");

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Edit));
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RequestCode(ForgotPassword model)
        {
            if (_accountApplication.CheckMobile(model.Mobile))
            {
                var code = _accountApplication.SendChangePasswordCode(model.Mobile);
                var codeGenerationTime = DateTime.Now;
                _accountService.Set(model.Mobile, codeGenerationTime, code);

                model.Message = ValidationMessage.SmsSent;
                return RedirectToAction(nameof(ForgotPassword));
            }

            model.Message = ValidationMessage.RecordNotFound;
            return RedirectToAction(nameof(ForgotPassword));
        }

        [HttpPost]
        public IActionResult CheckCode(ForgotPassword model)
        {
            var info = _accountService.Get();

            if (!_accountApplication.CheckExpiration(info.codeGenerationTime))
            {
                if (_accountApplication.CheckCode(info.code, model.Code))
                {
                    var accountId = _accountApplication.GetAccountIdByMobile(info.mobile);
                    return RedirectToAction(nameof(ResetPassword), new { Id = accountId });
                }

                model.Message = ValidationMessage.WrongCode;
                return RedirectToAction(nameof(ForgotPassword));
            }

            model.Message = ValidationMessage.CodeExpired;
            return RedirectToAction(nameof(ForgotPassword));
        }

        [HttpGet]
        public IActionResult ResetPassword(long id)
        {
            var model = new ResetPassword() { Id = id, RedirectionTime = DateTime.Now };
            return View(model);
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPassword model)
        {
            if (!_accountApplication.CheckExpiration(model.RedirectionTime))
            {
                var operationResult = _accountApplication.ResetPassword(model);
                if (operationResult.IsSuccess)
                    return RedirectToAction(nameof(Index), "Home");

                ViewBag.ErrorMessage = operationResult.Message;
                return RedirectToAction(nameof(Index), "Home");
            }

            ViewBag.ErrorMessage = ValidationMessage.CodeExpired;
            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            var accountId = _accountApplication.GetCurrentAccountId();
            var model = new UserChangePassword() { Id = accountId };
            return View(model);
        }

        [HttpPost]
        public IActionResult ChangePassword(UserChangePassword model)
        {
            var operationResult = _accountApplication.ChangePassword(model);

            if (operationResult.IsSuccess)
                return RedirectToAction(nameof(Index), "Home");

            ViewBag.ErrorMessage = operationResult.Message;
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
