using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Data
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName).HasMaxLength(225).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(225).IsRequired(required: false);
            builder.Property(x => x.Mobile).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(225).IsRequired();
            builder.Property(x => x.PicturePath).HasMaxLength(1000).IsRequired(required: false);

            builder.HasOne(x => x.Role)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.RoleId);

            builder.OwnsMany(x => x.Permissions, modelBuilder =>
            {
                modelBuilder.ToTable("AccountPermissions");
                modelBuilder.HasKey(x => x.Id);
                modelBuilder.Ignore(x => x.Name);

                modelBuilder.WithOwner(x => x.Account).HasForeignKey(x => x.AccountId);
            });
        }
    }
}
