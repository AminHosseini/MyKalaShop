using InventoryManagement.Application.Contracts.Inventory;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infrastructure.InventoryAcl
{
    public class ShopInventoryAcl : IShopInventoryAcl
    {
        private readonly IInventoryApplication _inventoryApplication;

        public ShopInventoryAcl(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        public bool RemoveFromInventory(List<OrderItem> orderItems)
        {
            var list = new List<DecreaseInventory>();

            foreach (var orderItem in orderItems)
            {
                var decreaseItem = new DecreaseInventory(orderItem.Count, "خرید مشتری", orderItem.OrderId, orderItem.ProductId);
                list.Add(decreaseItem);
            }

            var result = _inventoryApplication.Decrease(list);
            return result.IsSuccess;
        }
    }
}
