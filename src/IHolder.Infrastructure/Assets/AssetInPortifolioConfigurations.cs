using IHolder.Domain.Assets;
using IHolder.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IHolder.Infrastructure.Assets;
public class AssetInPortifolioConfigurations : EntityConfiguration<AssetInPortfolio>
{
    public override void Configure(EntityTypeBuilder<AssetInPortfolio> builder)
    {
        builder.Property(p => p.AssetId).IsRequired();
        builder.Property(p => p.AveragePrice).IsRequired();
        builder.Property(p => p.Quantity).IsRequired();
        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.FirstInvestmentDate).IsRequired();
        builder.ToTable("AssetInPortfolio");
    }
}
