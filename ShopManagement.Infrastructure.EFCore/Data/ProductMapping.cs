using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InventoryManagement.Domain.ProductAgg;

namespace InventoryManagement.Infrastructure.EFCore.Data
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
            builder.Property(x => x.PicturePath).HasMaxLength(1000).IsRequired(required: false);
            builder.Property(x => x.PictureAlt).HasMaxLength(100).IsRequired(required: false);
            builder.Property(x => x.PictureTitle).HasMaxLength(225).IsRequired(required: false);
            builder.Property(x => x.ShortDescription).HasMaxLength(500).IsRequired(required: false);
            builder.Property(x => x.Description).HasMaxLength(1000).IsRequired(required: false);
            builder.Property(x => x.Keywords).HasMaxLength(225).IsRequired(required: false);
            builder.Property(x => x.Slug).HasMaxLength(225).IsRequired();
            builder.Property(x => x.MetaDescription).HasMaxLength(1000).IsRequired(required: false);
            builder.Property(x => x.ProductCategoryId).IsRequired();

            builder.HasOne(x => x.ProductCategory)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ProductCategoryId);

            builder.HasMany(x => x.ProductPictures)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
