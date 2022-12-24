using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Account;

namespace AccountManagement.Domain.AccountAgg
{
    public interface IAccountRepository : IRepository<long, Account>
    {
        List<AccountViewModel> Search(SearchAccount model);
        EditAccount GetDetails(long id);
        SpecifyAccountPermissions GetPermissions(long id);
    }
}
