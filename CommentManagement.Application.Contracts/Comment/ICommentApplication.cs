using _0_Framework.Application;

namespace CommentManagement.Application.Contracts.Comment
{
    public interface ICommentApplication
    {
        OperationResult Create(CreateComment model);
        OperationResult Confirm(long id);
        OperationResult Cancel(long id);
        List<CommentViewModel> Search(SearchComment model);
    }
}
