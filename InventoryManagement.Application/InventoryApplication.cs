using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _repository;

        public InventoryApplication(IInventoryRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Create(CreateInventory model)
        {
            var operationResult = new OperationResult();

            if (_repository.Exists(x => x.ProductId == model.ProductId && x.Color == model.Color
                && x.UnitPrice == model.UnitPrice))
                return operationResult.Failed();

            var inventory = new Inventory(model.ProductId, model.UnitPrice, model.Color);

            _repository.Create(inventory);
            _repository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Decrease(DecreaseInventory model)
        {
            var operationResult = new OperationResult();
            var inventory = _repository.Get(model.Id);

            if (inventory == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            inventory.Decrease(model.Count, 0, model.Description, 0);

            _repository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditInventory model)
        {
            var operationResult = new OperationResult();
            var inventory = _repository.Get(model.Id);

            if (inventory == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            if (_repository.Exists(x => x.ProductId == model.ProductId && x.Color == model.Color
                && x.UnitPrice == model.UnitPrice))
                return operationResult.Failed();

            inventory.Edit(model.ProductId, model.UnitPrice, model.Color);

            _repository.Save();
            return operationResult.Succeeded();
        }

        public EditInventory GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<InventoryOperationsLog> GetLog(long inventoryId)
        {
            return _repository.GetLog(inventoryId);
        }

        public OperationResult Increase(IncreaseInventory model)
        {
            var operationResult = new OperationResult();
            var inventory = _repository.Get(model.Id);

            if (inventory == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            inventory.Increase(model.Count, 0, model.Description);

            _repository.Save();
            return operationResult.Succeeded();
        }

        public List<InventoryViewModel> Search(SearchInventory model)
        {
            return _repository.Search(model);
        }
    }
}
