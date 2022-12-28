using MyKalaShopQuery.Contracts.Article;

namespace MyKalaShopQuery.Contracts.ArticleCategory
{
    public class ArticleCategoryQueryView
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public int ArticlesCount { get; set; }
        public List<string> KeywordsList { get; set; }
        public List<ArticleQueryView> Articles { get; set; }
    }
}
