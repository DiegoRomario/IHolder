using IHolder.Domain.Enumerators;
using IHolder.Domain.Products;

namespace IHolder.Domain.Allocations;

public class AllocationByProduct : Allocation
{
    private AllocationByProduct() { }
    public AllocationByProduct(Guid productId, Guid userId, decimal targetPercentage) : base(targetPercentage, userId)
    {
        ProductId = productId;
        Recommendation = Recommendation.Hold;
    }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = default!;
}
