using IHolder.Domain.Portfolios;
using System.Linq.Expressions;

namespace IHolder.Application.Common.Interfaces;

public interface IPortfolioRepository
{
    Task<Portfolio?> GetByPredicateAsync(Expression<Func<Portfolio, bool>> predicate, CancellationToken ct, bool includes = false);
    Task<Portfolio?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<bool> ExistsByPredicateAsync(Expression<Func<Portfolio, bool>> predicate, CancellationToken ct);
    Task AddAsync(Portfolio portfolio, CancellationToken ct);
    Task UpdateAsync(Portfolio portfolio, CancellationToken ct);
    Task<AssetInPortfolio?> GetAssetInPortfolioByIdAsync(Guid id, CancellationToken ct);
    Task<bool> ExistsAssetInPortfolioByPredicateAsync(Expression<Func<AssetInPortfolio, bool>> predicate, CancellationToken ct);
    Task<bool> HasAllocationsByAssetInPortfolioAsync(Guid assetInPortfolioId, CancellationToken ct);
    Task<List<Guid>> GetAllCategoryIdsInPortfolioByUserAsync(Guid userId, CancellationToken ct);
    Task<List<Guid>> GetAllProductIdsInPortfolioByUserAsync(Guid userId, CancellationToken ct);
    Task<List<Guid>> GetAllAssetIdsInPortfolioByUserAsync(Guid userId, CancellationToken ct);
    Task<decimal> GetInvestedAmountoByCategory(Guid userId, Guid categoryId, CancellationToken ct);
    Task<decimal> GetInvestedAmountoByAsset(Guid userId, Guid assetId, CancellationToken ct);
    Task<decimal> GetInvestedAmountoByProduct(Guid userId, Guid productId, CancellationToken ct);
    Task<decimal> GetInvestedAmount(Guid userId, CancellationToken ct);

}