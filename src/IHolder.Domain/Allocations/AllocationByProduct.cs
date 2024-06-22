using IHolder.Domain.Enumerators;
using IHolder.Domain.Products;

namespace IHolder.Domain.Allocations;

public class AllocationByProduct : Allocation
{
    private AllocationByProduct() { }
    public AllocationByProduct(Product product, Guid userId, decimal targetPercentage) : base(targetPercentage, userId)
    {
        Product = product;
        Recommendation = Recommendation.Hold;
    }
    public Product Product { get; private set; } = default!;
}
