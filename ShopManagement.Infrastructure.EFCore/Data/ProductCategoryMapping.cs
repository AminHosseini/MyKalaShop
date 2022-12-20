using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InventoryManagement.Domain.ProductCategoryAgg;

namespace InventoryManagement.Infrastructure.EFCore.Data
{
    public class ProductCategoryMapping : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Slug).HasMaxLength(225).IsRequired();
            builder.Property(x => x.PicturePath).HasMaxLength(1000).IsRequired(required: false);
            builder.Property(x => x.PictureAlt).HasMaxLength(100).IsRequired(required: false);
            builder.Property(x => x.PictureTitle).HasMaxLength(225).IsRequired(required: false);
            builder.Property(x => x.Description).HasMaxLength(1000).IsRequired(required: false);
            builder.Property(x => x.Keywords).HasMaxLength(225).IsRequired(required: false);
            builder.Property(x => x.MetaDescription).HasMaxLength(500).IsRequired(required: false);

            builder.HasMany(x => x.Products)
                .WithOne(x => x.ProductCategory)
                .HasForeignKey(x => x.ProductCategoryId);
        }
    }
}
