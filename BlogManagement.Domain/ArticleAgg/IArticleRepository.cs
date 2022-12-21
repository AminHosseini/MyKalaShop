using _0_Framework.Domain;
using BlogManagement.Application.Contracts.Article;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository : IRepository<long, Article>
    {
        List<ArticleViewModel> Search(SearchArticle model);
        EditArticle GetDetails(long id);
    }
}
