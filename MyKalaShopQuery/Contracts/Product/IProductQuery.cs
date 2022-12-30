using ShopManagement.Application.Contracts.Order;

namespace MyKalaShopQuery.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryView> GetProductsWithProductCategory();
        List<ProductQueryView> GetLatestArrivals();
        ProductQueryView GetProductBySlug(string slug);
        List<ProductQueryView> Search(string key);
        List<CartItem> CheckInventoryStatus(List<CartItem> cartItems);
    }
}
