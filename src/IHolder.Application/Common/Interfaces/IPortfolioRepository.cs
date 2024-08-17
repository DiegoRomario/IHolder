using IHolder.Domain.Portfolios;
using System.Linq.Expressions;

namespace IHolder.Application.Common.Interfaces;

public interface IPortfolioRepository
{
    Task<Portfolio?> GetByUserIdAsync(Guid userId, CancellationToken ct);
    Task<bool> ExistsByPredicateAsync(Expression<Func<Portfolio, bool>> predicate, CancellationToken ct);
    Task AddAsync(Portfolio portfolio, CancellationToken ct);
    Task UpdateAsync(Portfolio portfolio, CancellationToken ct);
    Task AddAssetAsync(AssetInPortfolio assetInPortfolio, CancellationToken ct);
    Task UpdateAssetAsync(AssetInPortfolio assetInPortfolio, CancellationToken ct);
    Task RemoveAssetAsync(AssetInPortfolio assetInPortfolio, CancellationToken ct);
    Task<AssetInPortfolio?> GetAssetByIdAsync(Guid id, CancellationToken ct);
    Task<bool> ExistsAssetByPredicateAsync(Expression<Func<AssetInPortfolio, bool>> predicate, CancellationToken ct);
}