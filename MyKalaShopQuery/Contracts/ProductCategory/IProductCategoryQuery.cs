namespace MyKalaShopQuery.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        List<ProductCategoryQueryView> GetProductCategories();
        ProductCategoryQueryView GetProductCategoriesWithProductsBySlug(string slug);
    }
}
