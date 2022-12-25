namespace MyKalaShopQuery.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        List<ProductCategoryQueryView> GetProductCategories();
        List<ProductCategoryQueryView> GetProductCategoryWithProducts();
    }
}
