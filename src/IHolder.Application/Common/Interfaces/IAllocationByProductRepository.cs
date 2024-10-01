using IHolder.Domain.Allocations;

namespace IHolder.Application.Common.Interfaces;

public interface IAllocationByProductRepository : IAllocationRepository
{
    Task<AllocationByProduct?> GetByIdAsync(Guid id, CancellationToken ct);
}
