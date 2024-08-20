using IHolder.Domain.Allocations;
using IHolder.Domain.Categories.Events;

namespace IHolder.Application.Allocations.Mappers;

public static class AllocationCommandsMapping
{
    public static AllocationByCategory ToEntity(this CategoryCreatedEvent categoryCreatedEvent, Guid portfolioId, decimal targetPercentage = 0)
    {
        return new AllocationByCategory(categoryCreatedEvent.CategoryId, portfolioId, targetPercentage);
    }
}

