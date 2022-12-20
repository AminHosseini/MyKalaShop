using _0_Framework.Application;

namespace InventoryManagement.Application.Contracts.ProductCategory
{
    public interface IProductCategoryApplication
    {
        OperationResult Create(CreateProductCategory model);
        OperationResult Edit(EditProductCategory model);
        List<ProductCategoryViewModel> GetProductCategories();
        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> Search(SearchProductCategory model);
    }
}
