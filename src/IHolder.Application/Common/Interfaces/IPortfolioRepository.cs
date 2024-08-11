using IHolder.Domain.Portfolios;
using System.Linq.Expressions;

namespace IHolder.Application.Common.Interfaces;

public interface IPortfolioRepository
{
    Task<Portfolio?> GetByUserIdAsync(Guid userId, CancellationToken ct);
    Task<bool> ExistsByPredicateAsync(Expression<Func<Portfolio, bool>> predicate, CancellationToken ct);
    Task AddAsync(Portfolio portfolio, CancellationToken ct);
    Task UpdateAsync(Portfolio portfolio, CancellationToken ct);
}