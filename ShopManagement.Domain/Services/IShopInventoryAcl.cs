using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Domain.Services
{
    public interface IShopInventoryAcl
    {
        bool RemoveFromInventory(List<OrderItem> orderItems);
    }
}
