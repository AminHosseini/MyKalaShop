namespace BlogManagement.Infrastructure.Configuration.Permissions
{
    public static class BlogPermissions
    {
        public const int AccessToBlogManagement = 500;

        public const int AccessToArticleCategories = 510;
        public const int ListArticleCategories = 511;
        public const int SearchArticleCategories = 512;
        public const int CreateArticleCategory = 513;
        public const int EditArticleCategory = 514;

        public const int AccessToArticles = 520;
        public const int ListArticles = 521;
        public const int SearchArticles = 522;
        public const int CreateArticle = 523;
        public const int EditArticle = 524;
    }
}
