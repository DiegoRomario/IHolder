using IHolder.Domain.Portfolios;
using IHolder.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IHolder.Infrastructure.Portfolios;

public class PortfolioConfigurations : EntityConfiguration<Portfolio>
{
    public override void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.Name).HasColumnType("VARCHAR(80)").IsRequired();

        builder.HasOne(p => p.User)
               .WithOne(u => u.Portfolio)
               .HasForeignKey<Portfolio>(p => p.UserId);

        builder.HasMany(p => p.AssetsInPortfolio)
               .WithOne(a => a.Portfolio)
               .HasForeignKey(a => a.PortfolioId);

        builder.HasMany(p => p.AllocationsByCategory)
               .WithOne(a => a.Portfolio)
               .HasForeignKey(a => a.PortfolioId);

        builder.HasMany(p => p.AllocationsByProduct)
               .WithOne(a => a.Portfolio)
               .HasForeignKey(a => a.PortfolioId);

        builder.HasMany(p => p.AllocationsByAsset)
               .WithOne(a => a.Portfolio)
               .HasForeignKey(a => a.PortfolioId);

        builder.ToTable("Portfolio");
    }
}
