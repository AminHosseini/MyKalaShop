﻿using _0_Framework.Infrastructure;
using BlogManagement.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.Configuration.Permissions;
using BlogManagement.Infrastructure.EFCore.Data;
using BlogManagement.Infrastructure.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyKalaShopQuery.Contracts.Article;
using MyKalaShopQuery.Contracts.ArticleCategory;
using MyKalaShopQuery.Query;

namespace BlogManagement.Infrastructure.Configuration
{
    public static class BlogManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<BlogContext>(options => options.UseSqlServer(connectionString));

            services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();
            services.AddTransient<IArticleCategoryQuery, ArticleCategoryQuery>();

            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<IArticleApplication, ArticleApplication>();
            services.AddTransient<IArticleQuery, ArticleQuery>();

            services.AddTransient<IPermissionExposer, BlogPermissionExposer>();
        }
    }
}
