namespace MyKalaShopQuery.Contracts.Article
{
    public interface IArticleQuery
    {
        List<ArticleQueryView> GetLatestArticles();
        ArticleQueryView GetArticleBySlug(string slug);
    }
}
