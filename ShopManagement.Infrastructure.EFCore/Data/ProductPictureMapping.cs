using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EFCore.Data
{
    public class ProductPictureMapping : IEntityTypeConfiguration<ProductPicture>
    {
        public void Configure(EntityTypeBuilder<ProductPicture> builder)
        {
            builder.ToTable("ProductPictures");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PicturePath).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.PictureAlt).HasMaxLength(100).IsRequired(required: false);
            builder.Property(x => x.PictureTitle).HasMaxLength(225).IsRequired(required: false);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductPictures)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
