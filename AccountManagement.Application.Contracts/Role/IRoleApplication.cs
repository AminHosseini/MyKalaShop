using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Role
{
    public interface IRoleApplication
    {
        OperationResult Create(CreateRole model);
        OperationResult Edit(EditRole model);
        List<RoleViewModel> GetRoles();
        EditRole GetDetails(long id);
    }
}
