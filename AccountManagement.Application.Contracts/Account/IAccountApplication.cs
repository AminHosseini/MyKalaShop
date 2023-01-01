using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Account
{
    public interface IAccountApplication
    {
        OperationResult Create(CreateAccount model);
        OperationResult Edit(EditAccount model);
        OperationResult ChangePassword(ChangePassword model);
        OperationResult SpecifyPermissions(SpecifyAccountPermissions model);
        List<AccountViewModel> Search(SearchAccount model);
        EditAccount GetDetails(long id);
        SpecifyAccountPermissions GetPermissions(long id);
        OperationResult Login(Login model);
        void Logout();
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        EditAccount GetLoggedInUserDetails();
        bool CheckMobile(string mobile);
        int SendChangePasswordCode(string mobile);
        bool CheckExpiration(DateTime codeGenerationTime);
        bool CheckCode(int generatedCode, int codeEnteredByUser);
        long GetAccountIdByMobile(string mobile);
        OperationResult ResetPassword(ResetPassword model);
        long GetCurrentAccountId();
        OperationResult ChangePassword(UserChangePassword model);
    }
}
