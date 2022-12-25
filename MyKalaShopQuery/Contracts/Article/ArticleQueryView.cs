namespace MyKalaShopQuery.Contracts.Article
{
    public class ArticleQueryView
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string PublishDate { get; set; }
        public string PicturePath { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string ShortDescription { get; set; }
    }
}
