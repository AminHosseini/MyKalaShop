using CommentManagement.Application;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EFCore.Data;
using CommentManagement.Infrastructure.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommentManagement.Infrastructure.Configuration
{
    public static class CommentManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<CommentContext>(options => options.UseSqlServer(connectionString));

            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<ICommentApplication, CommentApplication>();
        }
    }
}
