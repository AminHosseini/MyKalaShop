using ShopManagement.Application.Contracts.Order;

namespace MyKalaShopQuery.Contracts
{
    public interface ICartCalculatorService
    {
        Cart ComputeCart(List<CartItem> cartItems);
    }
}
