using IHolder.Domain.Allocations;
using IHolder.Domain.Portfolios;
using IHolder.Domain.Products;
using IHolder.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IHolder.Infrastructure.Allocations;
public class AllocationByProductConfigurations : EntityConfiguration<AllocationByProduct>
{
    public override void Configure(EntityTypeBuilder<AllocationByProduct> builder)
    {
        base.Configure(builder);

        builder.ToTable("AllocationByProduct");

        builder.Property(p => p.PortfolioId).IsRequired()
                                            .HasColumnOrder(2);

        builder.Property(p => p.ProductId).IsRequired()
                                          .HasColumnOrder(3);

        builder.ComplexProperty(a => a.AllocationValues, i =>
        {
            i.Property(a => a.CurrentAmount).HasColumnName("CurrentAmount")
                                            .HasColumnType("DECIMAL(18,4)")
                                            .IsRequired()
                                            .HasColumnOrder(4);

            i.Property(a => a.TargetPercentage).HasColumnName("TargetPercentage")
                                               .HasColumnType("DECIMAL(18,4)")
                                               .IsRequired()
                                               .HasColumnOrder(5);

            i.Property(a => a.CurrentPercentage).HasColumnName("CurrentPercentage")
                                                .HasColumnType("DECIMAL(18,4)")
                                                .IsRequired()
                                                .HasColumnOrder(6);

            i.Property(a => a.PercentageDifference).HasColumnName("PercentageDifference")
                                                   .HasColumnType("DECIMAL(18,4)")
                                                   .IsRequired()
                                                   .HasColumnOrder(7);

            i.Property(a => a.AmountDifference).HasColumnName("AmountDifference")
                                               .HasColumnType("DECIMAL(18,4)")
                                               .IsRequired()
                                               .HasColumnOrder(8);
        });


        builder.Property(p => p.Recommendation).IsRequired()
                                               .HasColumnType("TINYINT")
                                               .HasColumnOrder(9);

        builder.HasOne<Portfolio>().WithMany(p => p.AllocationsByProduct).HasForeignKey(p => p.PortfolioId);

        builder.HasOne<Product>().WithMany().HasForeignKey(p => p.ProductId);

    }

}

