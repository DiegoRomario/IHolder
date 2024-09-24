using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using MediatR;

namespace IHolder.Application.Allocations.UpdateByCategory;

public class AllocationByCategoryUpdateCommandHandler(IAllocationRepository _repository) :
             IRequestHandler<AllocationByCategoryUpdateCommand, ErrorOr<AllocationByCategory>>
{
    public async Task<ErrorOr<AllocationByCategory>> Handle(AllocationByCategoryUpdateCommand request, CancellationToken ct)
    {
        var allocationByCategory = await _repository.GetByPredicateAsync<AllocationByCategory>(p => p.Id == request.Id, ct);

        if (allocationByCategory is null) return Error.NotFound(description: "Allocation by category not found");

        allocationByCategory.AllocationValues.UpdateTargetPercentage(request.TargetPercentage);

        await _repository.UpdateAsync(allocationByCategory, ct);

        allocationByCategory = await _repository.GetByIdAsync<AllocationByCategory>(request.Id, ct);

        if (allocationByCategory == null) return Error.Conflict(description: "Failed to retrieve the updated allocation by category.");

        return allocationByCategory;
    }
}
