using _0_Framework.Application;

namespace InventoryManagement.Application.Contracts.ProductPicture
{
    public interface IProductPictureApplication
    {
        OperationResult Create(CreateProductPicture model);
        OperationResult Edit(EditProductPicture model);
        OperationResult Delete(long id);
        OperationResult Restore(long id);
        List<ProductPictureViewModel> Search(SearchProductPicture model);
        EditProductPicture GetDetails(long id);
    }
}
