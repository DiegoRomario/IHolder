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
        builder.Property(a => a.Description).HasColumnType("VARCHAR(80)").IsRequired();
        builder.Property(a => a.Details).HasColumnType("VARCHAR(600)").IsRequired();
        builder.Property(a => a.Ticker).HasColumnType("VARCHAR(80)").IsRequired();
        builder.Property(a => a.Price).IsRequired();
        builder.Property(a => a.ProductId).IsRequired();
        builder.Property(p => p.State).HasColumnType("TINYINT");
        builder.Property(p => p.StateSetAt).IsRequired();
        builder.ToTable("Asset");
    }
}
