using MyKalaShopQuery.Contracts.Comment;

namespace MyKalaShopQuery.Contracts.Article
{
    public class ArticleQueryView
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string PublishDate { get; set; }
        public string PicturePath { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public List<string> KeywordsList { get; set; }

        public long ArticleCategoryId { get; set; }
        public string ArticleCategory { get; set; }
        public string ArticleCategorySlug { get; set; }

        public List<CommentQueryView> ArticleComments { get; set; }
    }
}
