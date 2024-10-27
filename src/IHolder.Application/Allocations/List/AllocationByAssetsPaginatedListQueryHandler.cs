using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Allocations.List;

public class AllocationByAssetsPaginatedListQueryHandler(IPortfolioRepository _repository) : IRequestHandler<AllocationByAssetsPaginatedListQuery, ErrorOr<PaginatedList<AllocationByAsset>>>
{
    public async Task<ErrorOr<PaginatedList<AllocationByAsset>>> Handle(AllocationByAssetsPaginatedListQuery request, CancellationToken ct)
    {
        return await _repository.GetAllocationsPaginatedAsync(request.Filter, ct);
    }
}