using IHolder.Application.Allocations.UpdateByCategory;
using IHolder.Contracts.Allocations;
using IHolder.Domain.Allocations;

namespace IHolder.API.Allocations;

public static class AllocationContractsMapping
{
    public static AllocationByCategoryResponse ToResponse(this AllocationByCategory allocation)
    {
        return new AllocationByCategoryResponse(
            allocation.Id,
            allocation.CategoryId,
            allocation.Category.Name,
            allocation.Category.Description,
            (byte)allocation.Recommendation,
            allocation.AllocationValues.CurrentAmount,
            allocation.AllocationValues.TargetPercentage,
            allocation.AllocationValues.CurrentPercentage,
            allocation.AllocationValues.PercentageDifference,
            allocation.AllocationValues.AmountDifference,
            allocation.CreatedAt,
            allocation.UpdatedAt);
    }

    public static AllocationByCategoryUpdateCommand ToCommand(this AllocationByCategoryUpdateRequest request, Guid id)
    {
        return new AllocationByCategoryUpdateCommand(id, request.TargetPercentage);
    }
}
