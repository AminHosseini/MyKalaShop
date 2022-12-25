namespace MyKalaShopQuery.Contracts.Article
{
    public interface IArticleQuery
    {
        List<ArticleQueryView> GetLatestArticles();
    }
}
