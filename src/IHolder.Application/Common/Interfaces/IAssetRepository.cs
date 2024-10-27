using IHolder.Application.Assets.List;
using IHolder.Domain.Assets;
using IHolder.SharedKernel.DTO;
using System.Linq.Expressions;

namespace IHolder.Application.Common.Interfaces;

public interface IAssetRepository
{
    Task<Asset?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<bool> ExistsByPredicateAsync(Expression<Func<Asset, bool>> predicate, CancellationToken ct);
    Task<PaginatedList<Asset>> GetPaginatedAsync(AssetsPaginatedListFilter filter, CancellationToken ct);
    Task AddAsync(Asset asset, CancellationToken ct);
    Task UpdateAsync(Asset asset, CancellationToken ct);
    Task<string?> GetExchangeIdByAssetTickerAsync(string ticker, CancellationToken ct);
}