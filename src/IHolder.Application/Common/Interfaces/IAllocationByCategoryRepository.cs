using IHolder.Application.Allocations.List;
using IHolder.Domain.Allocations;
using IHolder.SharedKernel.DTO;

namespace IHolder.Application.Common.Interfaces;

public interface IAllocationByCategoryRepository : IAllocationRepository
{
    Task<AllocationByCategory?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<PaginatedList<AllocationByCategory>> GetPaginatedAsync(AllocationByCategoriesPaginatedListFilter filter, CancellationToken ct);
}
