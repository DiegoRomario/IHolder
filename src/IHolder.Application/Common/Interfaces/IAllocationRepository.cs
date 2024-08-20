using IHolder.Domain.Allocations;

namespace IHolder.Application.Common.Interfaces;

public interface IAllocationRepository
{
    Task AddAsync(AllocationByCategory allocation, CancellationToken ct);
}
