using Microsoft.EntityFrameworkCore;
using InventoryManagement.Domain.ProductAgg;
using InventoryManagement.Domain.ProductCategoryAgg;
using InventoryManagement.Domain.ProductPictureAgg;
using InventoryManagement.Domain.SlideAgg;

namespace InventoryManagement.Infrastructure.EFCore.Data
{
    public class ShopContext : DbContext
    {
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<Slide> Slides { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(ProductCategoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
