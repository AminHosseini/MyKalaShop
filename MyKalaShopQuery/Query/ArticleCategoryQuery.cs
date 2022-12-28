using _0_Framework.Application;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using MyKalaShopQuery.Contracts.Article;
using MyKalaShopQuery.Contracts.ArticleCategory;

namespace MyKalaShopQuery.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogContext _context;

        public ArticleCategoryQuery(BlogContext context)
        {
            _context = context;
        }

        public ArticleCategoryQueryView GetArticleCategoryBySlug(string slug)
        {
            var articleCategory = _context.ArticleCategories
                .Include(x => x.Articles)
                .ThenInclude(x => x.ArticleCategory)
                .Select(x => new ArticleCategoryQueryView()
                {
                    Name = x.Name,
                    Slug = x.Slug,
                    Keywords = x.Keywords,
                    Articles = MapArticles(x.Articles, x.Id)
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            if (!string.IsNullOrWhiteSpace(articleCategory.Keywords))
                articleCategory.KeywordsList = articleCategory.Keywords.Split(',').ToList();

            return articleCategory;
        }

        private static List<ArticleQueryView> MapArticles(List<Article> articles, long articleCategoryId)
        {
            return articles
                .Where(x => x.PublishDate <= DateTime.Now)
                .Where(x => x.ArticleCategoryId == articleCategoryId)
                .Select(x => new ArticleQueryView()
                {
                    Title = x.Title,
                    Slug = x.Slug,
                    ShortDescription = x.ShortDescription,
                    PicturePath = x.PicturePath,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PublishDate = x.PublishDate.ToFarsi()
                }).ToList();
        }

        public List<ArticleCategoryQueryView> GetArticleCategories()
        {
            return _context.ArticleCategories
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryView()
                {
                    Name = x.Name,
                    Slug = x.Slug,
                    ArticlesCount = x.Articles.Where(y => y.PublishDate <= DateTime.Now && y.ArticleCategoryId == x.Id).Count()
                }).AsNoTracking().ToList();
        }
    }
}
