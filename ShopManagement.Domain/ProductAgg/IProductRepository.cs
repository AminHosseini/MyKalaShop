using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Product;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<long, Product>
    {
        List<ProductViewModel> Search(SearchProduct model);
        EditProduct GetDetails(long id);
    }
}
