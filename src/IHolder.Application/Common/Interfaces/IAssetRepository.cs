using IHolder.Application.Allocations.List;
using IHolder.Application.Assets.List;
using IHolder.Domain.Allocations;
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

    // Allocation
    Task<bool> ExistsAllocationByPredicateAsync(Expression<Func<AllocationByAsset, bool>> predicate, CancellationToken ct);
    Task<AllocationByAsset?> GetAllocationByPredicateAsync(Expression<Func<AllocationByAsset, bool>> predicate, CancellationToken ct);
    Task AddAllocationAsync(AllocationByAsset allocation, CancellationToken ct);
    Task UpdateAllocationAsync(AllocationByAsset allocation, CancellationToken ct);
    Task DeleteAllocationAsync(AllocationByAsset allocation, CancellationToken ct);
    Task<AllocationByAsset?> GetAllocationByIdAsync(Guid id, CancellationToken ct);
    Task<PaginatedList<AllocationByAsset>> GetAllocationsPaginatedAsync(AllocationByAssetsPaginatedListFilter filter, CancellationToken ct);
}