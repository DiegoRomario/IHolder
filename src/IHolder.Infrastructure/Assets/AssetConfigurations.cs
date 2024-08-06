using IHolder.Domain.Assets;
using IHolder.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IHolder.Infrastructure.Assets;
public class AssetConfigurations : EntityConfiguration<Asset>
{
    public override void Configure(EntityTypeBuilder<Asset> builder)
    {
        base.Configure(builder);

        builder.ToTable("Asset");

        builder.Property(a => a.ProductId).IsRequired()
                                          .HasColumnOrder(2);

        builder.Property(a => a.Name).HasColumnType("VARCHAR(80)")
                                     .IsRequired()
                                     .HasColumnOrder(3);

        builder.Property(a => a.Description).HasColumnType("VARCHAR(600)")
                                            .IsRequired()
                                            .HasColumnOrder(4);

        builder.Property(a => a.Ticker).HasColumnType("VARCHAR(80)")
                                       .IsRequired()
                                       .HasColumnOrder(5);

        builder.Property(a => a.Price).IsRequired()
                                      .HasColumnOrder(6);

    }
}
