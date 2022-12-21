using _0_Framework.Application;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _repository;

        public CommentApplication(ICommentRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Cancel(long id)
        {
            var operationResult = new OperationResult();
            var comment = _repository.Get(id);

            if (comment == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            comment.Cancel();
            _repository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Confirm(long id)
        {
            var operationResult = new OperationResult();
            var comment = _repository.Get(id);

            if (comment == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            comment.Confirm();
            _repository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Create(CreateComment model)
        {
            var operationResult = new OperationResult();

            var comment = new Comment(model.Name, model.Email, model.Message,
                model.OwnerRecordId, model.CommentType, model.Website, model.ParentId);

            _repository.Create(comment);
            _repository.Save();
            return operationResult.Succeeded();
        }

        public List<CommentViewModel> Search(SearchComment model)
        {
            return _repository.Search(model);
        }
    }
}
