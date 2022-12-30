using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using _0_Framework.Infrastructure;
using MyKalaShopQuery.Query;
using MyKalaShopQuery.Contracts.Slide;
using MyKalaShopQuery.Contracts.ProductCategory;
using MyKalaShopQuery.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.EFCore.Data;
using ShopManagement.Infrastructure.EFCore.Repositories;
using ShopManagement.Infrastructure.Configuration.Permissions;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Application;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.Services;
using ShopManagement.Infrastructure.InventoryAcl;
using MyKalaShopQuery.Contracts;
using _0_Framework.Application.ZarinPal;

namespace ShopManagement.Infrastructure.Configuration
{
    public static class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<ShopContext>(options => options.UseSqlServer(connectionString));

            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductApplication, ProductApplication>();
            services.AddTransient<IProductQuery, ProductQuery>();

            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();
            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();

            services.AddTransient<ISlideRepository, SlideRepository>();
            services.AddTransient<ISlideApplication, SlideApplication>();
            services.AddTransient<ISlideQuery, SlideQuery>();

            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderApplication, OrderApplication>();

            services.AddTransient<IShopInventoryAcl, ShopInventoryAcl>();

            services.AddSingleton<ICartService, CartService>();
            services.AddTransient<ICartCalculatorService, CartCalculatorService>();
            services.AddTransient<IZarinPalFactory, ZarinPalFactory>();

            services.AddTransient<IPermissionExposer, ShopPermissionExposer>();
        }
    }
}
