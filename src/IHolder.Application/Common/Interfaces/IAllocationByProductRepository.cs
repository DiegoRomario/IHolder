using IHolder.Application.Allocations.List;
using IHolder.Domain.Allocations;
using IHolder.SharedKernel.DTO;

namespace IHolder.Application.Common.Interfaces;

public interface IAllocationByProductRepository : IAllocationRepository
{
    Task<AllocationByProduct?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<PaginatedList<AllocationByProduct>> GetPaginatedAsync(AllocationByProductsPaginatedListFilter filter, CancellationToken ct);
}
