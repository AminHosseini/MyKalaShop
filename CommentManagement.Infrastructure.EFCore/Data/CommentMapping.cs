using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentManagement.Infrastructure.EFCore.Data
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(225).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(225).IsRequired();
            builder.Property(x => x.Message).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Website).HasMaxLength(500).IsRequired(required: false);
        }
    }
}
