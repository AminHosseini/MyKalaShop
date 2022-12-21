using _0_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _repository;
        private readonly IFileUploader _fileUploader;

        public ArticleCategoryApplication(IArticleCategoryRepository repository, IFileUploader fileUploader)
        {
            _repository = repository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticleCategory model)
        {
            var operationResult = new OperationResult();

            if (_repository.Exists(x => x.Name == model.Name))
                return operationResult.Failed();

            var slug = model.Slug.Slugify();
            var pictureRoot = $"UploadedPictures/{slug}";
            var picturePath = _fileUploader.Upload(model.PicturePath, pictureRoot);

            var articleCategory = new ArticleCategory(model.Name, slug, picturePath,
                model.PictureAlt, model.PictureTitle, model.Description, model.Keywords,
                model.MetaDescription, model.CanonicalAddress, model.DisplayOrder);

            _repository.Create(articleCategory);
            _repository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditArticleCategory model)
        {
            var operationResult = new OperationResult();
            var articleCategory = _repository.Get(model.Id);

            if (articleCategory == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            if (_repository.Exists(x => x.Name == model.Name && x.Id != model.Id))
                return operationResult.Failed();

            var slug = model.Slug.Slugify();
            var pictureRoot = $"UploadedPictures/{slug}";
            var picturePath = _fileUploader.Upload(model.PicturePath, pictureRoot);

            articleCategory.Edit(model.Name, slug, picturePath,
                model.PictureAlt, model.PictureTitle, model.Description, model.Keywords,
                model.MetaDescription, model.CanonicalAddress, model.DisplayOrder);

            _repository.Save();
            return operationResult.Succeeded();
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<ArticleCategoryViewModel> Search(SearchArticleCategory model)
        {
            return _repository.Search(model);
        }
    }
}
