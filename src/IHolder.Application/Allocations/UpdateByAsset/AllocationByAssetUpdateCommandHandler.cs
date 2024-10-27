using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using MediatR;

namespace IHolder.Application.Allocations.UpdateByAsset;

public class AllocationByAssetUpdateCommandHandler(IAssetRepository _repository) :
             IRequestHandler<AllocationByAssetUpdateCommand, ErrorOr<AllocationByAsset>>
{
    public async Task<ErrorOr<AllocationByAsset>> Handle(AllocationByAssetUpdateCommand request, CancellationToken ct)
    {
        var allocationByAsset = await _repository.GetAllocationByPredicateAsync(p => p.Id == request.Id, ct);

        if (allocationByAsset is null) return Error.NotFound(description: "Allocation by asset not found");

        allocationByAsset.AllocationValues.UpdateTargetPercentage(request.TargetPercentage);

        await _repository.UpdateAllocationAsync(allocationByAsset, ct);

        allocationByAsset = await _repository.GetAllocationByIdAsync(request.Id, ct);

        if (allocationByAsset == null) return Error.Conflict(description: "Failed to retrieve the updated allocation by asset.");

        return allocationByAsset;
    }
}
