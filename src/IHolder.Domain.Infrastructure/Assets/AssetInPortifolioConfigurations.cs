using IHolder.Domain.Assets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IHolder.Domain.Infrastructure.Assets;
public class AssetInPortifolioConfigurations : IEntityTypeConfiguration<AssetInPortfolio>
{
    public void Configure(EntityTypeBuilder<AssetInPortfolio> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(p => p.AssetId).IsRequired();
        builder.Property(p => p.AveragePrice).IsRequired();
        builder.Property(p => p.Quantity).IsRequired();
        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.FirstInvestmentDate).IsRequired();
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt);
        builder.ToTable("AssetInPortfolio");
    }
}
