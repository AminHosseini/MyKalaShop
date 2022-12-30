using _0_Framework.Application;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory model);
        OperationResult Edit(EditInventory model);
        OperationResult Increase(IncreaseInventory model);
        OperationResult Decrease(DecreaseInventory model);
        OperationResult Decrease(List<DecreaseInventory> model);
        List<InventoryViewModel> Search(SearchInventory model);
        EditInventory GetDetails(long id);
        List<InventoryOperationsLog> GetLog(long inventoryId);
    }
}
