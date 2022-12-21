using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Account
{
    public interface IAccountApplication
    {
        OperationResult Create(CreateAccount model);
        OperationResult Edit(EditAccount model);
        OperationResult ChangePassword(AccountChangePassword model);
        List<AccountViewModel> Search(SearchAccount model);
        EditAccount GetDetails(long id);
    }
}
