using _0_Framework.Application;
using _0_Framework.Infrastructure;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EFCore.Data;

namespace CommentManagement.Infrastructure.EFCore.Repositories
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
        private readonly CommentContext _context;

        public CommentRepository(CommentContext context) : base(context)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(SearchComment model)
        {
            var query = _context.Comments.Select(x => new CommentViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Message = x.Message.Substring(0, Math.Min(x.Message.Length, 50)) + "...",
                Email = x.Email,
                IsConfirmed = x.IsConfirmed,
                IsCanceled = x.IsCanceled,
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(model.Name))
                query = query.Where(x => x.Name.Contains(model.Name));

            if (!string.IsNullOrWhiteSpace(model.Email))
                query = query.Where(x => x.Email.Contains(model.Email));

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
