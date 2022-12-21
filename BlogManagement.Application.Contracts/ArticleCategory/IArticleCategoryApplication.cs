using _0_Framework.Application;

namespace BlogManagement.Application.Contracts.ArticleCategory
{
    public interface IArticleCategoryApplication
    {
        OperationResult Create(CreateArticleCategory model);
        OperationResult Edit(EditArticleCategory model);
        List<ArticleCategoryViewModel> Search(SearchArticleCategory model);
        EditArticleCategory GetDetails(long id);
    }
}
