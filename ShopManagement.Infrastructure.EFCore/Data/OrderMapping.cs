using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EFCore.Data
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.IssueTrackingNo).HasMaxLength(10).IsRequired(required: false);

            builder.OwnsMany(x => x.OrderItems, modelBuilder =>
            {
                modelBuilder.ToTable("OrderItems");
                modelBuilder.HasKey(x => x.Id);

                modelBuilder.WithOwner(x => x.Order).HasForeignKey(x => x.OrderId);
            });
        }
    }
}
