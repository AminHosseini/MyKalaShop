using _0_Framework.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleRepository _repository;
        private readonly IFileUploader _fileUploader;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleApplication(IArticleRepository repository,
            IFileUploader fileUploader, IArticleCategoryRepository articleCategoryRepository)
        {
            _repository = repository;
            _fileUploader = fileUploader;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public OperationResult Create(CreateArticle model)
        {
            var operationResult = new OperationResult();

            if (_repository.Exists(x => x.Title == model.Title))
                return operationResult.Failed();

            var slug = model.Slug.Slugify();
            var articleCategorySlug = _articleCategoryRepository.GetSlugById(model.ArticleCategoryId);
            var pictureRoot = $"UploadedPictures/{articleCategorySlug}/{slug}";
            var picturePath = _fileUploader.Upload(model.PicturePath, pictureRoot);
            var publishDate = model.PublishDate.ToGeorgianDateTime();

            var article = new Article(model.Title, slug, publishDate, picturePath,
                model.PictureAlt, model.PictureTitle, model.ShortDescription, model.Description,
                model.Keywords, model.MetaDescription, model.CanonicalAddress, model.ArticleCategoryId);

            _repository.Create(article);
            _repository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditArticle model)
        {
            var operationResult = new OperationResult();
            var article = _repository.Get(model.Id);

            if (article == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            if (_repository.Exists(x => x.Title == model.Title && x.Id != model.Id))
                return operationResult.Failed();

            var slug = model.Slug.Slugify();
            var articleCategorySlug = _articleCategoryRepository.GetSlugById(model.ArticleCategoryId);
            var pictureRoot = $"UploadedPictures/{articleCategorySlug}/{slug}";
            var picturePath = _fileUploader.Upload(model.PicturePath, pictureRoot);
            var publishDate = model.PublishDate.ToGeorgianDateTime();

            article.Edit(model.Title, slug, publishDate, picturePath,
                model.PictureAlt, model.PictureTitle, model.ShortDescription, model.Description,
                model.Keywords, model.MetaDescription, model.CanonicalAddress, model.ArticleCategoryId);

            _repository.Save();
            return operationResult.Succeeded();
        }

        public EditArticle GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<ArticleViewModel> Search(SearchArticle model)
        {
            return _repository.Search(model);
        }
    }
}
