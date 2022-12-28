using _0_Framework.Application;
using BlogManagement.Infrastructure.EFCore.Data;
using CommentManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using MyKalaShopQuery.Contracts.Article;
using MyKalaShopQuery.Contracts.Comment;

namespace MyKalaShopQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _context;
        private readonly CommentContext _commentContext;

        public ArticleQuery(BlogContext context, CommentContext commentContext)
        {
            _context = context;
            _commentContext = commentContext;
        }

        public ArticleQueryView GetArticleBySlug(string slug)
        {
            var article = _context.Articles
                .Include(x => x.ArticleCategory)
                .Select(x => new ArticleQueryView()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Slug = x.Slug,
                    Description = x.Description,
                    PictureAlt = x.PictureAlt,
                    PicturePath = x.PicturePath,
                    PictureTitle = x.PictureTitle,
                    ArticleCategoryId = x.ArticleCategoryId,
                    ArticleCategory = x.ArticleCategory.Name,
                    ArticleCategorySlug = x.ArticleCategory.Slug,
                    PublishDate = x.PublishDate.ToFarsi(),
                    Keywords = x.Keywords
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            if (!string.IsNullOrWhiteSpace(article.Keywords))
                article.KeywordsList = article.Keywords.Split(',').ToList();

            var comments = _commentContext.Comments
                .Where(x => !x.IsCanceled && x.IsConfirmed)
                .Where(x => x.Type == CommentType.ArticleComment)
                .Where(x => x.OwnerRecordId == article.Id)
                .Select(x => new CommentQueryView()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Message = x.Message,
                    ParentId = x.ParentId,
                    CreationDate = x.CreationDate.ToFarsi()
                }).AsNoTracking().ToList();

            foreach (var comment in comments)
            {
                if (comment.ParentId != 0)
                    comment.Parent = comments.FirstOrDefault(x => x.Id == comment.ParentId)?.Name;
            }

            article.ArticleComments = comments;
            return article;
        }

        public List<ArticleQueryView> GetLatestArticles()
        {
            return _context.Articles
                .Where(x => x.PublishDate <= DateTime.Now)
                .Select(x => new ArticleQueryView()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Slug = x.Slug,
                    ShortDescription = x.ShortDescription,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PicturePath = x.PicturePath,
                    PublishDate = x.PublishDate.ToFarsi()
                }).AsNoTracking().OrderByDescending(x => x.Id).Take(6).ToList();
        }
    }
}
