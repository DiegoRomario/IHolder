using IHolder.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IHolder.Domain.Infrastructure.Users;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(p => p.FirstName).HasColumnType("VARCHAR(80)").IsRequired();
        builder.Property(p => p.LastName).HasColumnType("VARCHAR(80)").IsRequired();
        builder.Property(p => p.Email).HasColumnType("VARCHAR(40)").IsRequired();
        builder.Property("_passwordHash").HasColumnName("PasswordHash");
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt);

        builder.HasAlternateKey(a => a.Email);

        builder.HasMany(p => p.AllocationsByCategory).WithOne(a => a.User).HasForeignKey(a => a.UserId);
        builder.HasMany(p => p.AllocationsByProduct).WithOne(a => a.User).HasForeignKey(a => a.UserId);
        builder.HasMany(p => p.AllocationsByAsset).WithOne(a => a.User).HasForeignKey(a => a.UserId);
        builder.HasMany(p => p.AssetsInPortfolio).WithOne(a => a.User).HasForeignKey(a => a.UserId);

        builder.ToTable("User");
    }
}
