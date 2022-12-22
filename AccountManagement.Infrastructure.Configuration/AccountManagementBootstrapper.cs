using AccountManagement.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastructure.EFCore.Data;
using AccountManagement.Infrastructure.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagement.Infrastructure.Configuration
{
    public static class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<AccountContext>(options => options.UseSqlServer(connectionString));

            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountApplication, AccountApplication>();

            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IRoleApplication, RoleApplication>();
        }
    }
}
