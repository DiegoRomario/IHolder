using IHolder.Domain.Categories;
using IHolder.Domain.Enumerators;

namespace IHolder.Domain.Allocations;

public class AllocationByCategory : Allocation
{
    private AllocationByCategory() { }

    public AllocationByCategory(Guid categoryId, Guid portfolioId, decimal targetPercentage, Guid? id = null) : base(targetPercentage, portfolioId, id ?? Guid.NewGuid())
    {
        CategoryId = categoryId;
        Recommendation = Recommendation.Hold;
    }

    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = default!;
}