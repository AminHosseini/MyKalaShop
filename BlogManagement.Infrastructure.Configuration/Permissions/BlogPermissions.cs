namespace BlogManagement.Infrastructure.Configuration.Permissions
{
    public static class BlogPermissions
    {
        public const int AccessToBlogManagement = 500;

        public const int ListArticleCategories = 501;
        public const int SearchArticleCategories = 502;
        public const int CreateArticleCategory = 503;
        public const int EditArticleCategory = 504;

        public const int ListArticles = 510;
        public const int SearchArticles = 511;
        public const int CreateArticle = 512;
        public const int EditArticle = 513;
    }
}
