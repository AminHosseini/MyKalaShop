using _0_Framework.Application;

namespace InventoryManagement.Application.Contracts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct model);
        OperationResult Edit(EditProduct model);
        List<ProductViewModel> Search(SearchProduct model);
        EditProduct GetDetails(long id);
        List<ProductViewModel> GetProducts();
    }
}
