using MyKalaShopQuery.Contracts.ArticleCategory;
using MyKalaShopQuery.Contracts.ProductCategory;

namespace MyKalaShopQuery
{
    public class MenuModel
    {
        public List<ProductCategoryQueryView> ProductCategories { get; set; }
        public List<ArticleCategoryQueryView> ArticleCategories { get; set; }
    }
}
