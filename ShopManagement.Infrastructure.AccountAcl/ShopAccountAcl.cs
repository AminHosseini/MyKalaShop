using AccountManagement.Infrastructure.EFCore.Data;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infrastructure.AccountAcl
{
    public class ShopAccountAcl : IShopAccountAcl
    {
        private readonly AccountContext _accountContext;

        public ShopAccountAcl(AccountContext accountContext)
        {
            _accountContext = accountContext;
        }

        public (string FullName, string Mobile) GetAccountInfo(long id)
        {
            var account = _accountContext.Accounts.FirstOrDefault(x => x.Id == id);
            return (account.FullName, account.Mobile);
        }
    }
}