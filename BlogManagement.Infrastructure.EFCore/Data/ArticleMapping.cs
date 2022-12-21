using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EFCore.Data
{
    public class ArticleMapping : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).HasMaxLength(225).IsRequired();
            builder.Property(x => x.Slug).HasMaxLength(225).IsRequired();
            builder.Property(x => x.PublishDate).IsRequired();
            builder.Property(x => x.PicturePath).HasMaxLength(1000).IsRequired(required: false);
            builder.Property(x => x.PictureAlt).HasMaxLength(100).IsRequired(required: false);
            builder.Property(x => x.PictureTitle).HasMaxLength(225).IsRequired(required: false);
            builder.Property(x => x.ShortDescription).HasMaxLength(300).IsRequired(required: false);
            builder.Property(x => x.Description).IsRequired(required: false);
            builder.Property(x => x.Keywords).HasMaxLength(225).IsRequired(required: false);
            builder.Property(x => x.MetaDescription).HasMaxLength(1000).IsRequired(required: false);
            builder.Property(x => x.CanonicalAddress).HasMaxLength(225).IsRequired(required: false);

            builder.HasOne(x => x.ArticleCategory)
                .WithMany(x => x.Articles)
                .HasForeignKey(x => x.ArticleCategoryId);
        }
    }
}
