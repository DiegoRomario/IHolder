﻿using IHolder.Domain.Portfolios;
using IHolder.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IHolder.Infrastructure.Portfolios;

public class AssetInPortfolioConfigurations : EntityConfiguration<AssetInPortfolio>
{
    public override void Configure(EntityTypeBuilder<AssetInPortfolio> builder)
    {
        builder.Property(p => p.AssetId).IsRequired();
        builder.Property(p => p.PortfolioId).IsRequired();
        builder.HasOne<Portfolio>().WithMany(p => p.AssetsInPortfolio).HasForeignKey(p => p.PortfolioId);
        builder.Property(p => p.AveragePrice).IsRequired();
        builder.Property(p => p.Quantity).IsRequired();
        builder.Property(p => p.FirstInvestmentDate).IsRequired();
        builder.Property(p => p.State).HasColumnType("TINYINT").IsRequired();
        builder.Property(p => p.StateSetAt).IsRequired();
        builder.ToTable("AssetInPortfolio");
    }
}
