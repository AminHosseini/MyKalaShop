using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Account
{
    public interface IAccountApplication
    {
        OperationResult Create(CreateAccount model);
        OperationResult Edit(EditAccount model);
        OperationResult ChangePassword(AccountChangePassword model);
        OperationResult SpecifyPermissions(SpecifyAccountPermissions model);
        List<AccountViewModel> Search(SearchAccount model);
        EditAccount GetDetails(long id);
        SpecifyAccountPermissions GetPermissions(long id);
        OperationResult Login(LoginViewModel model);
        void Logout();
    }
}
