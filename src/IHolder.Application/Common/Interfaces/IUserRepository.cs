using IHolder.Domain.Users;
using System.Linq.Expressions;

namespace IHolder.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByPredicateAsync(Expression<Func<User, bool>> predicate, CancellationToken ct);
    Task<bool> ExistsByPredicateAsync(Expression<Func<User, bool>> predicate, CancellationToken ct);
    Task AddAsync(User user, CancellationToken ct);
    Task UpdateAsync(User user, CancellationToken ct);
}