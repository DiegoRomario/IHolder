using ErrorOr;
using IHolder.Application.Common;
using IHolder.Domain.Allocations;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Allocations.List;

public class AllocationByCategoriesPaginatedListQueryHandler(ICategoryRepository _repository) : IRequestHandler<AllocationByCategoriesPaginatedListQuery, ErrorOr<PaginatedList<AllocationByCategory>>>
{
    public async Task<ErrorOr<PaginatedList<AllocationByCategory>>> Handle(AllocationByCategoriesPaginatedListQuery request, CancellationToken ct)
    {
        return await _repository.GetAllocationsPaginatedAsync(request.Filter, ct);
    }
}