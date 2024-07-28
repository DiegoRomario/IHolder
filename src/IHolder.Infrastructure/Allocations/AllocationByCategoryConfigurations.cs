using IHolder.Domain.Allocations;
using IHolder.Domain.Categories;
using IHolder.Domain.Portfolios;
using IHolder.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IHolder.Infrastructure.Allocations;
public class AllocationByCategoryConfigurations : EntityConfiguration<AllocationByCategory>
{
    public override void Configure(EntityTypeBuilder<AllocationByCategory> builder)
    {
        base.Configure(builder);
        builder.ComplexProperty(a => a.AllocationValues, i =>
        {
            i.Property(a => a.TargetPercentage).HasColumnName("TargetPercentage").HasColumnType("DECIMAL(18,4)").IsRequired();
            i.Property(a => a.CurrentPercentage).HasColumnName("CurrentPercentage").HasColumnType("DECIMAL(18,4)").IsRequired();
            i.Property(a => a.PercentageDifference).HasColumnName("PercentageDifference").HasColumnType("DECIMAL(18,4)").IsRequired();
            i.Property(a => a.CurrentAmount).HasColumnName("CurrentAmount").HasColumnType("DECIMAL(18,4)").IsRequired();
            i.Property(a => a.AmountDifference).HasColumnName("AmountDifference").HasColumnType("DECIMAL(18,4)").IsRequired();
        });
        builder.Property(p => p.PortfolioId).IsRequired();
        builder.HasOne<Portfolio>().WithMany(p => p.AllocationsByCategory).HasForeignKey(p => p.PortfolioId);

        builder.Property(p => p.CategoryId).IsRequired();
        builder.HasOne<Category>().WithMany().HasForeignKey(p => p.CategoryId);

        builder.Property(p => p.Recommendation).IsRequired().HasColumnType("TINYINT");
        builder.ToTable("AllocationByCategory");

    }

}

