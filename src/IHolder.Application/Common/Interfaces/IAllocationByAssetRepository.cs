using IHolder.Application.Allocations.List;
using IHolder.Domain.Allocations;
using IHolder.SharedKernel.DTO;

namespace IHolder.Application.Common.Interfaces;

public interface IAllocationByAssetRepository : IAllocationRepository
{
    Task<AllocationByAsset?> GetByIdAsync(Guid id, CancellationToken ct);

    Task<PaginatedList<AllocationByAsset>> GetPaginatedAsync(AllocationByAssetsPaginatedListFilter filter, CancellationToken ct);
}
