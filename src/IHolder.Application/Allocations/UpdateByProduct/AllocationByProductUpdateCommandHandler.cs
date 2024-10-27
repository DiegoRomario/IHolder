using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using MediatR;

namespace IHolder.Application.Allocations.UpdateByProduct;

public class AllocationByProductUpdateCommandHandler(IProductRepository _repository) :
             IRequestHandler<AllocationByProductUpdateCommand, ErrorOr<AllocationByProduct>>
{
    public async Task<ErrorOr<AllocationByProduct>> Handle(AllocationByProductUpdateCommand request, CancellationToken ct)
    {
        var allocationByProduct = await _repository.GetAllocationByPredicateAsync(p => p.Id == request.Id, ct);

        if (allocationByProduct is null) return Error.NotFound(description: "Allocation by product not found");

        allocationByProduct.AllocationValues.UpdateTargetPercentage(request.TargetPercentage);

        await _repository.UpdateAllocationAsync(allocationByProduct, ct);

        allocationByProduct = await _repository.GetAllocationByIdAsync(request.Id, ct);

        if (allocationByProduct == null) return Error.Conflict(description: "Failed to retrieve the updated allocation by product.");

        return allocationByProduct;
    }
}
