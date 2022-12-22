using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _repository;

        public RoleApplication(IRoleRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Create(CreateRole model)
        {
            var operationResult = new OperationResult();

            if (_repository.Exists(x => x.Name == model.Name))
                return operationResult.Failed();

            var role = new Role(model.Name);

            _repository.Create(role);
            _repository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditRole model)
        {
            var operationResult = new OperationResult();
            var role = _repository.Get(model.Id);

            if (role == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            if (_repository.Exists(x => x.Name == model.Name && x.Id != model.Id))
                return operationResult.Failed();

            role.Edit(model.Name);
            _repository.Save();
            return operationResult.Succeeded();
        }

        public EditRole GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<RoleViewModel> GetRoles()
        {
            return _repository.GetRoles();
        }
    }
}
