using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EFCore.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EFCore.Repositories
{
    public class ArticleRepository : RepositoryBase<long, Article>, IArticleRepository
    {
        private readonly BlogContext _context;

        public ArticleRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public EditArticle GetDetails(long id)
        {
            return _context.Articles.Select(x => new EditArticle()
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                CanonicalAddress = x.CanonicalAddress,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ShortDescription = x.ShortDescription,
                ArticleCategoryId = x.ArticleCategoryId,
                PublishDate = x.PublishDate.ToFarsi()
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleViewModel> Search(SearchArticle model)
        {
            var query = _context.Articles
                .Include(x => x.ArticleCategory)
                .Select(x => new ArticleViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    ShortDescription = x.ShortDescription,
                    ArticleCategoryId = x.ArticleCategoryId,
                    PicturePath = x.PicturePath,
                    ArticleCategory = x.ArticleCategory.Name,
                    PublishDate = x.PublishDate.ToFarsi()
                });

            if (!string.IsNullOrWhiteSpace(model.Title))
                query = query.Where(x => x.Title.Contains(model.Title));

            if (model.ArticleCategoryId != 0)
                query = query.Where(x => x.ArticleCategoryId == model.ArticleCategoryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
