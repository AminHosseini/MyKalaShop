using _0_Framework.Domain;
using InventoryManagement.Application.Contracts.ProductPicture;

namespace InventoryManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepository<long, ProductPicture>
    {
        List<ProductPictureViewModel> Search(SearchProductPicture model);
        EditProductPicture GetDetails(long id);
    }
}
