using IHolder.Domain.Assets;
using System.Linq.Expressions;

namespace IHolder.Application.Common.Interfaces;

public interface IAssetRepository
{
    Task<Asset?> GetByIdAsync(Guid id);
    Task<bool> ExistsByPredicateAsync(Expression<Func<Asset, bool>> predicate);
    Task AddAsync(Asset asset);
    Task UpdateAsync(Asset asset);
}