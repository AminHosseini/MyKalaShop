using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Infrastructure.EFCore.Data;

namespace AccountManagement.Infrastructure.EFCore.Repositories
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AccountContext _context;

        public AccountRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public EditAccount GetDetails(long id)
        {
            return _context.Accounts.Select(x => new EditAccount()
            {
                Id = x.Id,
                FullName = x.FullName,
                UserName = x.UserName,
                Mobile = x.Mobile,
                RoleId = 0
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<AccountViewModel> Search(SearchAccount model)
        {
            var query = _context.Accounts.Select(x => new AccountViewModel()
            {
                Id = x.Id,
                FullName = x.FullName,
                UserName = x.UserName,
                Mobile = x.Mobile,
                PicturePath = x.PicturePath,
                RoleId = 0,
                Role = "فعلا هیچی",
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(model.FullName))
                query = query.Where(x => x.FullName.Contains(model.FullName));

            if (!string.IsNullOrWhiteSpace(model.UserName))
                query = query.Where(x => x.UserName.Contains(model.UserName));

            if (!string.IsNullOrWhiteSpace(model.Mobile))
                query = query.Where(x => x.Mobile.Contains(model.Mobile));

            if (model.RoleId != 0)
                query = query.Where(x => x.RoleId == model.RoleId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
