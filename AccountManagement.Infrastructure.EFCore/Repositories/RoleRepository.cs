using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastructure.EFCore.Data;

namespace AccountManagement.Infrastructure.EFCore.Repositories
{
    public class RoleRepository : RepositoryBase<long, Role>, IRoleRepository
    {
        private readonly AccountContext _context;

        public RoleRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public EditRole GetDetails(long id)
        {
            return _context.Roles.Select(x => new EditRole()
            {
                Id = x.Id,
                Name = x.Name
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<RoleViewModel> GetRoles()
        {
            return _context.Roles.Select(x => new RoleViewModel()
            {
                Id=x.Id,
                Name=x.Name,
                CreationDate = x.CreationDate.ToFarsi()
            }).OrderByDescending(x => x.Id).ToList();
        }
    }
}
