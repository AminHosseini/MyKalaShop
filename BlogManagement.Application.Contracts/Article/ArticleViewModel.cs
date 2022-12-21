namespace BlogManagement.Application.Contracts.Article
{
    public class ArticleViewModel
    {
        public long Id { get; set; }
        public string PicturePath { get; set; }
        public string Title { get; set; }
        public long ArticleCategoryId { get; set; }
        public string ArticleCategory { get; set; }
        public string ShortDescription { get; set; }
        public string PublishDate { get; set; }
    }
}
