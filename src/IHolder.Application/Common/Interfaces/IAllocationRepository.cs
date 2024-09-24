using IHolder.Domain.Allocations;
using System.Linq.Expressions;

namespace IHolder.Application.Common.Interfaces;

public interface IAllocationRepository
{
    Task<bool> ExistsByPredicateAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken ct) where T : Allocation;
    Task<T?> GetByPredicateAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken ct) where T : Allocation;
    Task<T?> GetByIdAsync<T>(Guid id, CancellationToken ct) where T : Allocation;
    Task AddAsync<T>(T allocation, CancellationToken ct) where T : Allocation;
    Task UpdateAsync<T>(T allocation, CancellationToken ct) where T : Allocation;
}
