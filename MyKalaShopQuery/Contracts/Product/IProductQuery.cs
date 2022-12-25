namespace MyKalaShopQuery.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryView> GetProductsWithProductCategory();
        List<ProductQueryView> GetLatestArrivals();
    }
}
