namespace MyKalaShopQuery.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        ArticleCategoryQueryView GetArticleCategoryBySlug(string slug);
        List<ArticleCategoryQueryView> GetArticleCategories();
    }
}
