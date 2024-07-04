using IHolder.Domain.Enumerators;

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
}
