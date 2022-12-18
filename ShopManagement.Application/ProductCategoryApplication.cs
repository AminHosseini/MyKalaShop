using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository, IFileUploader fileUploader)
        {
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductCategory model)
        {
            var operationResult = new OperationResult();

            if (_productCategoryRepository.Exists(x => x.Name == model.Name))
                return operationResult.Failed();

            var slug = model.Slug?.Slugify();
            var pictureRoot = $"UploadedPictures/{slug}";
            var picturePath = _fileUploader.Upload(model.PicturePath, pictureRoot);

            var productCategory = new ProductCategory(model.Name, slug, picturePath,
                model.PictureAlt, model.PictureTitle, model.Description,
                model.Keywords, model.MetaDescription);

            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditProductCategory model)
        {
            var operationResult = new OperationResult();
            var productCategory = _productCategoryRepository.Get(model.Id);

            if (productCategory == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            if (_productCategoryRepository.Exists(x => x.Name == model.Name && x.Id != model.Id))
                return operationResult.Failed();

            var slug = model.Slug?.Slugify();
            var pictureRoot = $"UploadedPictures/{slug}";
            var picturePath = _fileUploader.Upload(model.PicturePath, pictureRoot);

            productCategory.Edit(model.Name, model.Slug, picturePath, model.PictureAlt,
                model.PictureTitle, model.Description, model.Keywords, model.MetaDescription);

            _productCategoryRepository.Save();
            return operationResult.Succeeded();
        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _productCategoryRepository.GetProductCategories();
        }

        public List<ProductCategoryViewModel> Search(SearchProductCategory model)
        {
            return _productCategoryRepository.Search(model);
        }
    }
}
