using _0_Framework.Application;
using _0_Framework.Application.Sms;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository _repository;
        private readonly IFileUploader _fileUploader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthHelper _authHelper;
        private readonly ISmsService _smsService;

        public AccountApplication(IAccountRepository repository,
            IFileUploader fileUploader, IPasswordHasher passwordHasher, 
            IAuthHelper authHelper, ISmsService smsService)
        {
            _repository = repository;
            _fileUploader = fileUploader;
            _passwordHasher = passwordHasher;
            _authHelper = authHelper;
            _smsService = smsService;
        }

        public OperationResult Create(CreateAccount model)
        {
            var operationResult = new OperationResult();

            if (_repository.Exists(x => x.FullName == model.FullName && x.Mobile == model.Mobile))
                return operationResult.Failed();

            var pictureRoot = $"UploadedPictures/AccountPhotos/{model.Mobile}";
            var pciturePath = _fileUploader.Upload(model.PicturePath, pictureRoot);
            var password = _passwordHasher.Hash(model.Password);

            var account = new Account(model.FullName, model.UserName, model.Mobile, password, pciturePath, model.RoleId);

            _repository.Create(account);
            _repository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditAccount model)
        {
            var operationResult = new OperationResult();
            var account = _repository.Get(model.Id);

            if (account == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            if (_repository.Exists(x => x.FullName == model.FullName &&
                x.Mobile == model.Mobile && x.Id != model.Id))
                return operationResult.Failed();

            var pictureRoot = $"UploadedPictures/AccountPhotos/{model.Mobile}";
            var pciturePath = _fileUploader.Upload(model.PicturePath, pictureRoot);

            account.Edit(model.FullName, model.UserName, model.Mobile, pciturePath, model.RoleId);

            _repository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult ChangePassword(ChangePassword model)
        {
            var operationResult = new OperationResult();
            var account = _repository.Get(model.Id);

            if (account == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            var password = _passwordHasher.Hash(model.NewPassword);
            account.ChangePassword(password);

            _repository.Save();
            return operationResult.Succeeded();
        }

        public EditAccount GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<AccountViewModel> Search(SearchAccount model)
        {
            return _repository.Search(model);
        }

        public SpecifyAccountPermissions GetPermissions(long id)
        {
            return _repository.GetPermissions(id);
        }

        public OperationResult SpecifyPermissions(SpecifyAccountPermissions model)
        {
            var operationResult = new OperationResult();
            var account = _repository.Get(model.Id);

            if (account == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            var permissions = new List<Permission>();
            model.Permissions.ForEach(code => permissions.Add(new Permission(code)));

            account.SpecifyPermissions(permissions);
            _repository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Login(Login model)
        {
            var operationResult = new OperationResult();

            var account = _repository.GetAccountByUserName(model.UserName);
            if (account == null)
                return operationResult.Failed(ValidationMessage.WrongUserPass);

            var password = _passwordHasher.Check(account.Password, model.Password);
            if (!password.Verified)
                return operationResult.Failed(ValidationMessage.WrongUserPass);

            var permissions = account.Permissions.Select(x => x.Code).ToList();
            var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.FullName, account.UserName, account.Mobile, permissions);

            _authHelper.Signin(authViewModel);
            return operationResult.Succeeded();
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public EditAccount GetLoggedInUserDetails()
        {
            var accountId = _authHelper.CurrentAccountId();
            return GetDetails(accountId);
        }

        public bool CheckMobile(string mobile)
        {
            return _repository.GetAccountIdByMobile(mobile) != 0 ? true : false;
        }

        public int SendChangePasswordCode(string mobile)
        {
            var rand = new Random();
            var code = rand.Next(10000, 99999);

            _smsService.Send(mobile, $"کد تغییر رمز عبور : {code}");
            return code;
        }

        public bool CheckExpiration(DateTime codeGenerationTime)
        {
            if (DateTime.Now.Date == codeGenerationTime.Date)
                if (DateTime.Now.Hour == codeGenerationTime.Hour)
                    if (DateTime.Now.Minute - codeGenerationTime.Minute <= 2.0)
                        return false;

            return true;
        }

        public bool CheckCode(int generatedCode, int codeEnteredByUser)
        {
            return generatedCode == codeEnteredByUser;
        }

        public long GetAccountIdByMobile(string mobile)
        {
            return _repository.GetAccountIdByMobile(mobile);
        }

        public OperationResult ResetPassword(ResetPassword model)
        {
            var operationResult = new OperationResult();
            var account = _repository.Get(model.Id);

            if (account == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            var password = _passwordHasher.Hash(model.NewPassword);
            account.ChangePassword(password);

            _repository.Save();
            return operationResult.Succeeded();
        }

        public long GetCurrentAccountId()
        {
            return _authHelper.CurrentAccountId();
        }

        public OperationResult ChangePassword(UserChangePassword model)
        {
            var operationResult = new OperationResult();
            var account = _repository.Get(model.Id);

            if (account == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            var oldPassword = _passwordHasher.Check(account.Password, model.OldPassword);
            if (!oldPassword.Verified)
                return operationResult.Failed(ValidationMessage.WrongOldPassword);

            var password = _passwordHasher.Hash(model.NewPassword);
            account.ChangePassword(password);

            _repository.Save();
            return operationResult.Succeeded();
        }
    }
}
