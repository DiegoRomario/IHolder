using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Allocations.List;

public class AllocationByCategoriesPaginatedListQueryHandler(IAllocationByCategoryRepository _repository) : IRequestHandler<AllocationByCategoriesPaginatedListQuery, ErrorOr<PaginatedList<AllocationByCategory>>>
{
    public async Task<ErrorOr<PaginatedList<AllocationByCategory>>> Handle(AllocationByCategoriesPaginatedListQuery request, CancellationToken ct)
    {
        return await _repository.GetPaginatedAsync(request.Filter, ct);
    }
}