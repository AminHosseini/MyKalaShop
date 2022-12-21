using _0_Framework.Domain;
using BlogManagement.Application.Contracts.ArticleCategory;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IRepository<long, ArticleCategory>
    {
        List<ArticleCategoryViewModel> Search(SearchArticleCategory model);
        EditArticleCategory GetDetails(long id);
    }
}
