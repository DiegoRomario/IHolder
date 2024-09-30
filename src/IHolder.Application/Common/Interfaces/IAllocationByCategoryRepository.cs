using IHolder.Application.Allocations.List;
using IHolder.Domain.Allocations;
using IHolder.SharedKernel.DTO;

namespace IHolder.Application.Common.Interfaces;

public interface IAllocationByCategoryRepository : IAllocationRepository
{
    Task<PaginatedList<AllocationByCategory>> GetPaginatedAsync(AllocationByCategoriesPaginatedListFilter filter, CancellationToken ct);
}
