using IHolder.Domain.Assets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IHolder.Domain.Infrastructure.Assets;
public class AssetConfigurations : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Description).HasColumnType("VARCHAR(80)").IsRequired();
        builder.Property(a => a.Details).HasColumnType("VARCHAR(600)").IsRequired();
        builder.Property(a => a.Ticker).HasColumnType("VARCHAR(80)").IsRequired();
        builder.Property(a => a.Price).IsRequired();
        builder.Property(a => a.ProductId).IsRequired();
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt);
        builder.Property(p => p.State).HasColumnType("TINYINT");
        builder.Property(p => p.StateSetAt).IsRequired();
        builder.ToTable("Asset");
    }
}
