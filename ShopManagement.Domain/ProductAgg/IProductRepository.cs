using _0_Framework.Domain;
using InventoryManagement.Application.Contracts.Product;

namespace InventoryManagement.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<long, Product>
    {
        List<ProductViewModel> Search(SearchProduct model);
        EditProduct GetDetails(long id);
        List<ProductViewModel> GetProducts();
    }
}
