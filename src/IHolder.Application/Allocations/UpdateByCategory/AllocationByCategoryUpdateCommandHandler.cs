using ErrorOr;
using IHolder.Application.Common;
using IHolder.Domain.Allocations;
using MediatR;

namespace IHolder.Application.Allocations.UpdateByCategory;

public class AllocationByCategoryUpdateCommandHandler(ICategoryRepository _repository) :
             IRequestHandler<AllocationByCategoryUpdateCommand, ErrorOr<AllocationByCategory>>
{
    public async Task<ErrorOr<AllocationByCategory>> Handle(AllocationByCategoryUpdateCommand request, CancellationToken ct)
    {
        var allocationByCategory = await _repository.GetAllocationByPredicateAsync(p => p.Id == request.Id, ct);

        if (allocationByCategory is null) return Error.NotFound(description: "Allocation by category not found");

        allocationByCategory.AllocationValues.UpdateTargetPercentage(request.TargetPercentage);

        await _repository.UpdateAllocationAsync(allocationByCategory, ct);

        allocationByCategory = await _repository.GetAllocationByIdAsync(request.Id, ct);

        if (allocationByCategory == null) return Error.Conflict(description: "Failed to retrieve the updated allocation by category.");

        return allocationByCategory;
    }
}
