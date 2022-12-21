using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EFCore.Data
{
    public class ArticleCategoryMapping : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.ToTable("ArticleCategories");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Slug).HasMaxLength(225).IsRequired();
            builder.Property(x => x.PicturePath).HasMaxLength(1000).IsRequired(required: false);
            builder.Property(x => x.PictureAlt).HasMaxLength(100).IsRequired(required: false);
            builder.Property(x => x.PictureTitle).HasMaxLength(225).IsRequired(required: false);
            builder.Property(x => x.Description).HasMaxLength(1000).IsRequired(required: false);
            builder.Property(x => x.Keywords).HasMaxLength(225).IsRequired(required: false);
            builder.Property(x => x.MetaDescription).HasMaxLength(1000).IsRequired(required: false);
            builder.Property(x => x.CanonicalAddress).HasMaxLength(225).IsRequired(required: false);
        }
    }
}
