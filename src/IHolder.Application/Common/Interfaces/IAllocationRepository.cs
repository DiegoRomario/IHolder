using IHolder.Domain.Allocations;

namespace IHolder.Application.Common.Interfaces;

public interface IAllocationRepository
{
    Task AddAsync<T>(T allocation, CancellationToken ct) where T : Allocation;
}
