using IHolder.Application.Assets.List;
using IHolder.Domain.Assets;
using IHolder.SharedKernel.DTO;
using System.Linq.Expressions;

namespace IHolder.Application.Common.Interfaces;

public interface IAssetRepository
{
    Task<Asset?> GetByIdAsync(Guid id);
    Task<bool> ExistsByPredicateAsync(Expression<Func<Asset, bool>> predicate);
    Task<PaginatedList<Asset>> GetPaginatedAsync(AssetPaginatedListFilter filter);
    Task AddAsync(Asset asset);
    Task UpdateAsync(Asset asset);
}