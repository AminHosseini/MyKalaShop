using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Product;
using InventoryManagement.Domain.ProductAgg;
using InventoryManagement.Domain.ProductCategoryAgg;

namespace InventoryManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _repository;
        private readonly IFileUploader _fileUploader;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductApplication(IProductRepository repository,
            IFileUploader fileUploader, IProductCategoryRepository productCategoryRepository)
        {
            _repository = repository;
            _fileUploader = fileUploader;
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProduct model)
        {
            var operationResult = new OperationResult();

            if (_repository.Exists(x => x.Name == model.Name))
                return operationResult.Failed();

            var productCategoryateorySlug = _productCategoryRepository
                .GetProductCategorySlug(model.ProductCategoryId);
            var slug = model.Slug.Slugify();
            var pictureRoot = $"UploadedPictures/{productCategoryateorySlug}/{slug}";
            var picturePath = _fileUploader.Upload(model.PicturePath, pictureRoot);

            var product = new Product(model.Name, model.Code, picturePath, model.PictureAlt,
                model.PictureTitle, model.ShortDescription, model.Description, model.Keywords,
                slug, model.MetaDescription, model.ProductCategoryId);

            _repository.Create(product);
            _repository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditProduct model)
        {
            var operationResult = new OperationResult();
            var product = _repository.Get(model.Id);

            if (product == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            if (product.Name == model.Name && product.Id != model.Id)
                return operationResult.Failed();

            var productCategoryateorySlug = _productCategoryRepository
                .GetProductCategorySlug(model.ProductCategoryId);
            var slug = model.Slug.Slugify();
            var pictureRoot = $"UploadedPictures/{productCategoryateorySlug}/{slug}";
            var picturePath = _fileUploader.Upload(model.PicturePath, pictureRoot);

            product.Edit(model.Name, model.Code, picturePath, model.PictureAlt, model.PictureTitle,
                model.ShortDescription, model.Description, model.Keywords,
                slug, model.MetaDescription, model.ProductCategoryId);

            _repository.Save();
            return operationResult.Succeeded();
        }

        public EditProduct GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _repository.GetProducts();
        }

        public List<ProductViewModel> Search(SearchProduct model)
        {
            return _repository.Search(model);
        }
    }
}
