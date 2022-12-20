using _0_Framework.Domain;
using InventoryManagement.Application.Contracts.ProductCategory;

namespace InventoryManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<long, ProductCategory>
    {
        List<ProductCategoryViewModel> GetProductCategories();
        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> Search(SearchProductCategory model);
        string GetProductCategorySlug(long id);
    }
}
