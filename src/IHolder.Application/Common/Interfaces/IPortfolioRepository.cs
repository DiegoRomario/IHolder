using IHolder.Domain.Portfolios;
using System.Linq.Expressions;

namespace IHolder.Application.Common.Interfaces;

public interface IPortfolioRepository
{
    Task<Portfolio?> GetByPredicateAsync(Expression<Func<Portfolio, bool>> predicate, CancellationToken ct);
    Task<Portfolio?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<bool> ExistsByPredicateAsync(Expression<Func<Portfolio, bool>> predicate, CancellationToken ct);
    Task AddAsync(Portfolio portfolio, CancellationToken ct);
    Task UpdateAsync(Portfolio portfolio, CancellationToken ct);
    Task<AssetInPortfolio?> GetAssetInPortfolioByIdAsync(Guid id, CancellationToken ct);
    Task<bool> ExistsAssetInPortfolioByPredicateAsync(Expression<Func<AssetInPortfolio, bool>> predicate, CancellationToken ct);
    Task<bool> HasAllocationsByAssetInPortfolioAsync(Guid assetInPortfolioId, CancellationToken ct);
}