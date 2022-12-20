using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InventoryManagement.Domain.SlideAgg;

namespace InventoryManagement.Infrastructure.EFCore.Data
{
    public class SlideMapping : IEntityTypeConfiguration<Slide>
    {
        public void Configure(EntityTypeBuilder<Slide> builder)
        {
            builder.ToTable("Slides");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PicturePath).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.PictureAlt).HasMaxLength(100).IsRequired(required: false);
            builder.Property(x => x.PictureTitle).HasMaxLength(225).IsRequired(required: false);
            builder.Property(x => x.Heading).HasMaxLength(225).IsRequired(required: false);
            builder.Property(x => x.Title).HasMaxLength(225).IsRequired(required: false);
            builder.Property(x => x.Text).HasMaxLength(1000).IsRequired(required: false);
            builder.Property(x => x.ButtonText).HasMaxLength(225).IsRequired(required: false);
            builder.Property(x => x.Link).HasMaxLength(1000).IsRequired(required: false);
        }
    }
}
