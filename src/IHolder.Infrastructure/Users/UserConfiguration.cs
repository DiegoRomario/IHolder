using IHolder.Domain.Portfolios;
using IHolder.Domain.Users;
using IHolder.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IHolder.Infrastructure.Users;
public class UserConfiguration : EntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        builder.Property(p => p.FirstName).HasColumnType("VARCHAR(80)").IsRequired();
        builder.Property(p => p.LastName).HasColumnType("VARCHAR(80)").IsRequired();
        builder.Property(p => p.Email).HasColumnType("VARCHAR(80)").IsRequired();
        builder.Property("_passwordHash").HasColumnName("PasswordHash").HasColumnType("VARCHAR(1200)");
        builder.HasAlternateKey(a => a.Email);

        builder.HasOne(u => u.Portfolio)
               .WithOne(p => p.User)
               .HasForeignKey<Portfolio>(p => p.UserId);

        builder.ToTable("User");
    }
}
