using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _repository;
        private readonly IFileUploader _fileUploader;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductRepository _productRepository;

        public ProductPictureApplication(IProductPictureRepository repository, 
            IFileUploader fileUploader, IProductCategoryRepository productCategoryRepository,
            IProductRepository productRepository)
        {
            _repository = repository;
            _fileUploader = fileUploader;
            _productCategoryRepository = productCategoryRepository;
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProductPicture model)
        {
            var operationResult = new OperationResult();

            var product = _productRepository.Get(model.ProductId);
            var productCategorySlug = _productCategoryRepository.GetProductCategorySlug(product.ProductCategoryId);
            var productSlug = product.Slug;
            var pictureRoot = $"UploadedPictures/{productCategorySlug}/{productSlug}";
            var picturePath = _fileUploader.Upload(model.PicturePath, pictureRoot);

            var productPicture = new ProductPicture(picturePath, model.PictureAlt, model.PictureTitle, model.ProductId);
            _repository.Create(productPicture);
            _repository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditProductPicture model)
        {
            var operationResult = new OperationResult();
            var productPicture = _repository.Get(model.Id);

            if (productPicture == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            if (productPicture.Id != model.Id)
                return operationResult.Failed();

            var product = _productRepository.Get(productPicture.ProductId);
            var productCategorySlug = _productCategoryRepository.GetProductCategorySlug(product.ProductCategoryId);
            var productSlug = product.Slug;
            var pictureRoot = $"UploadedPictures/{productCategorySlug}/{productSlug}";
            var picturePath = _fileUploader.Upload(model.PicturePath, pictureRoot);

            productPicture.Edit(picturePath, model.PictureAlt, model.PictureTitle, model.ProductId);
            _repository.Save();
            return operationResult.Succeeded();
        }

        public EditProductPicture GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public OperationResult Delete(long id)
        {
            var operationResult = new OperationResult();
            var model = _repository.Get(id);

            if (model == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            model.Delete();
            _repository.Save();

            return operationResult.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operationResult = new OperationResult();
            var model = _repository.Get(id);

            if (model == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            model.Restore();
            _repository.Save();

            return operationResult.Succeeded();
        }

        public List<ProductPictureViewModel> Search(SearchProductPicture model)
        {
            return _repository.Search(model);
        }
    }
}
