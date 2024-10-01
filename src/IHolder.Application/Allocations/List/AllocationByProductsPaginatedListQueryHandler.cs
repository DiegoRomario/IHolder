using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Allocations.List;

public class AllocationByrRoductsPaginatedListQueryHandler(IAllocationByProductRepository _repository) : IRequestHandler<AllocationByProductsPaginatedListQuery, ErrorOr<PaginatedList<AllocationByProduct>>>
{
    public async Task<ErrorOr<PaginatedList<AllocationByProduct>>> Handle(AllocationByProductsPaginatedListQuery request, CancellationToken ct)
    {
        return await _repository.GetPaginatedAsync(request.Filter, ct);
    }
}