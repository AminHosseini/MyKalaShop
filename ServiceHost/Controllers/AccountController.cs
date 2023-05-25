using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ServiceHost.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountApplication _accountApplication;
        private readonly IAccountService _accountService;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountApplication accountApplication,
            IAccountService accountService, IConfiguration config, ITokenService tokenService)
        {
            _accountApplication = accountApplication;
            _accountService = accountService;
            _config = config;
            _tokenService = tokenService;
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

        //[HttpPost]
        //public IActionResult RequestCode(ForgotPassword model)
        //{
        //    if (_accountApplication.CheckMobile(model.Mobile))
        //    {
        //        var code = _accountApplication.SendChangePasswordCode(model.Mobile);
        //        var codeGenerationTime = DateTime.Now;
        //        _accountService.Set(model.Mobile, codeGenerationTime, code);

        //        model.Message = ValidationMessage.SmsSent;
        //        return RedirectToAction(nameof(ForgotPassword));
        //    }

        //    model.Message = ValidationMessage.RecordNotFound;
        //    return RedirectToAction(nameof(ForgotPassword));
        //}
        [HttpGet]
        public IActionResult RequestCode(string mobile)
        {
            if (_accountApplication.CheckMobile(mobile))
            {
                var code = _accountApplication.SendChangePasswordCode(mobile);
                var codeGenerationTime = DateTime.Now;
                _accountService.Set(mobile, codeGenerationTime, code);
            }

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
                    string token = CreateToken(info.mobile);
                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        _tokenService.Set(token);
                        return RedirectToAction(nameof(ResetPassword), new { Id = accountId, Token = token });
                    }
                }

                else
                    model.Message = ValidationMessage.WrongCode;
            }

            else
                model.Message = ValidationMessage.CodeExpired;

            return RedirectToAction(nameof(ForgotPassword));
        }

        [HttpGet]
        public IActionResult ResetPassword(long id, string token)
        {
            string mainToken = _tokenService.Get();

            if (token != null && mainToken != null)
            {
                if (mainToken == token)
                {
                    var model = new ResetPassword() { Id = id, RedirectionTime = DateTime.Now, Token = token };
                    return View(model);
                }
            }

            return RedirectToAction(nameof(ForgotPassword));
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPassword model)
        {
            string mainToken = _tokenService.Get();

            if (model.Token != null && mainToken != null)
            {
                if (mainToken == model.Token)
                {
                    if (!_accountApplication.CheckExpiration(model.RedirectionTime))
                    {
                        var operationResult = _accountApplication.ResetPassword(model);
                        if (operationResult.IsSuccess)
                            return RedirectToAction(nameof(Index), "Home");
                        else
                            ViewBag.ErrorMessage = operationResult.Message;
                    }
                    else
                        ViewBag.ErrorMessage = ValidationMessage.CodeExpired;
                }
                else
                    ViewBag.ErrorMessage = ValidationMessage.OperationFailed;
            }

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

        private string CreateToken(string mobile)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.MobilePhone, mobile)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddSeconds(120),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
