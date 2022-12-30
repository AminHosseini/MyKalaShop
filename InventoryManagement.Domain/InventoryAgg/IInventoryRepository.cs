using _0_Framework.Domain;
using InventoryManagement.Application.Contracts.Inventory;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository : IRepository<long, Inventory>
    {
        List<InventoryViewModel> Search(SearchInventory model);
        EditInventory GetDetails(long id);
        List<InventoryOperationsLog> GetLog(long inventoryId);
        Inventory GetInventoryByProductId(long productId);
    }
}
