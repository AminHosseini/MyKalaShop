using _0_Framework.Infrastructure;

namespace AccountManagement.Application.Contracts.Account
{
    public class SpecifyAccountPermissions
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<PermissionDto> MappedPermissions { get; set; }
        public List<int> Permissions { get; set; }
    }
}
