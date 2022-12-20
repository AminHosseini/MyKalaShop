using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using InventoryManagement.Application;
using InventoryManagement.Application.Contracts.Product;
using InventoryManagement.Application.Contracts.ProductCategory;
using InventoryManagement.Application.Contracts.ProductPicture;
using InventoryManagement.Application.Contracts.Slide;
using InventoryManagement.Domain.ProductAgg;
using InventoryManagement.Domain.ProductCategoryAgg;
using InventoryManagement.Domain.ProductPictureAgg;
using InventoryManagement.Domain.SlideAgg;
using InventoryManagement.Infrastructure.EFCore.Data;
using InventoryManagement.Infrastructure.EFCore.Repositories;

namespace InventoryManagement.Infrastructure.Configuration
{
    public static class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<ShopContext>(options => options.UseSqlServer(connectionString));

            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductApplication, ProductApplication>();

            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();
            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();

            services.AddTransient<ISlideRepository, SlideRepository>();
            services.AddTransient<ISlideApplication, SlideApplication>();
        }
    }
}
