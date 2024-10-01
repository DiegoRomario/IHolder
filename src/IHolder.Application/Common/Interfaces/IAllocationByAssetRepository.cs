using IHolder.Domain.Allocations;

namespace IHolder.Application.Common.Interfaces;

public interface IAllocationByAssetRepository : IAllocationRepository
{
    Task<AllocationByAsset?> GetByIdAsync(Guid id, CancellationToken ct);
}
