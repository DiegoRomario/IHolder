using IHolder.Domain.Categories;
using IHolder.Domain.Enumerators;

namespace IHolder.Domain.Allocations;

public class AllocationByCategory : Allocation
{
    private AllocationByCategory() { }
    public AllocationByCategory(Category category, Guid userId, decimal targetPercentage) : base(targetPercentage, userId)
    {
        Category = category;
        Recommendation = Recommendation.Hold;
    }
    public Category Category { get; private set; } = default!;
}
