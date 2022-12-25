using _0_Framework.Application;
using BlogManagement.Infrastructure.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using MyKalaShopQuery.Contracts.Article;

namespace MyKalaShopQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _context;

        public ArticleQuery(BlogContext context)
        {
            _context = context;
        }

        public List<ArticleQueryView> GetLatestArticles()
        {
            return _context.Articles.Select(x => new ArticleQueryView()
            {
                Title = x.Title,
                Slug = x.Slug,
                ShortDescription = x.ShortDescription,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PicturePath = x.PicturePath,
                PublishDate = x.PublishDate.ToFarsi()
            }).AsNoTracking().ToList();
        }
    }
}
