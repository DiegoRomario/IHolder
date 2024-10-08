﻿using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using MediatR;

namespace IHolder.Application.Allocations.UpdateByAsset;

public class AllocationByAssetUpdateCommandHandler(IAllocationByAssetRepository _repository) :
             IRequestHandler<AllocationByAssetUpdateCommand, ErrorOr<AllocationByAsset>>
{
    public async Task<ErrorOr<AllocationByAsset>> Handle(AllocationByAssetUpdateCommand request, CancellationToken ct)
    {
        var allocationByAsset = await _repository.GetByPredicateAsync<AllocationByAsset>(p => p.Id == request.Id, ct);

        if (allocationByAsset is null) return Error.NotFound(description: "Allocation by asset not found");

        allocationByAsset.AllocationValues.UpdateTargetPercentage(request.TargetPercentage);

        await _repository.UpdateAsync(allocationByAsset, ct);

        allocationByAsset = await _repository.GetByIdAsync(request.Id, ct);

        if (allocationByAsset == null) return Error.Conflict(description: "Failed to retrieve the updated allocation by asset.");

        return allocationByAsset;
    }
}
