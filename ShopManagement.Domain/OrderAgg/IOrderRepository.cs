using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Order;

namespace ShopManagement.Domain.OrderAgg
{
    public interface IOrderRepository : IRepository<long, Order>
    {
        List<OrderViewModel> Search(SearchOrders model);
        List<OrderItemViewModel> GetOrderItems(long orderId);
    }
}
