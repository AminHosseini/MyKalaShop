using _0_Framework.Application;

namespace BlogManagement.Application.Contracts.Article
{
    public interface IArticleApplication
    {
        OperationResult Create(CreateArticle model);
        OperationResult Edit(EditArticle model);
        List<ArticleViewModel> Search(SearchArticle model);
        EditArticle GetDetails(long id);
    }
}
