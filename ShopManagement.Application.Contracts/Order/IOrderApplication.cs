using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        List<OrderViewModel> Search(SearchOrders model);
        List<OrderItemViewModel> GetOrderItems(long orderId);
        long PlaceOrder(Cart cart);
        double GetAmountById(long id);
        OperationResult PaymentSucceeded(long orderId, long refId);
        string SetIssueTrackingNo(long orderId);
        void Cancel(long id);
    }
}
