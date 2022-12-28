using _0_Framework.Application;
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

        public AccountApplication(IAccountRepository repository,
            IFileUploader fileUploader, IPasswordHasher passwordHasher, IAuthHelper authHelper)
        {
            _repository = repository;
            _fileUploader = fileUploader;
            _passwordHasher = passwordHasher;
            _authHelper = authHelper;
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

        public OperationResult ChangePassword(AccountChangePassword model)
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

        public OperationResult Login(LoginViewModel model)
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
    }
}
