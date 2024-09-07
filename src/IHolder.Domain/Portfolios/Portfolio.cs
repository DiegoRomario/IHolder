using ErrorOr;
using IHolder.Domain.Allocations;
using IHolder.Domain.Common;
using IHolder.Domain.Portfolios.Events;
using IHolder.Domain.Users;

namespace IHolder.Domain.Portfolios;

public class Portfolio : AggregateRoot
{
    private readonly List<AssetInPortfolio> _assetsInPortfolio = [];
    private readonly List<AllocationByCategory> _allocationsByCategory = [];
    private readonly List<AllocationByProduct> _allocationsByProduct = [];
    private readonly List<AllocationByAsset> _allocationsByAsset = [];

    public Portfolio(Guid userId, string name, Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        Name = name;
    }

    private Portfolio() { }

    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public User User { get; private set; } = default!;
    public IReadOnlyCollection<AssetInPortfolio> AssetsInPortfolio => _assetsInPortfolio.AsReadOnly();
    public IReadOnlyCollection<AllocationByCategory> AllocationsByCategory => _allocationsByCategory.AsReadOnly();
    public IReadOnlyCollection<AllocationByProduct> AllocationsByProduct => _allocationsByProduct.AsReadOnly();
    public IReadOnlyCollection<AllocationByAsset> AllocationsByAsset => _allocationsByAsset.AsReadOnly();

    public ErrorOr<Success> AddAsset(AssetInPortfolio newAssetInPortfolio)
    {
        _assetsInPortfolio.Add(newAssetInPortfolio);
        _domainEvents.Add(new AssetInPortfolioAddedEvent(newAssetInPortfolio));

        return Result.Success;
    }

    public ErrorOr<Success> UpdateAsset(AssetInPortfolio updatedAssetInPortfolio)
    {
        var existingAssetInPortfolio = _assetsInPortfolio.SingleOrDefault(a => a.Id == updatedAssetInPortfolio.Id);

        if (existingAssetInPortfolio is null) return Error.NotFound(description: "Asset not found in portfolio assets");

        existingAssetInPortfolio.UpdateQuantity(updatedAssetInPortfolio.Quantity);
        existingAssetInPortfolio.UpdateAveragePrice(updatedAssetInPortfolio.AveragePrice);
        existingAssetInPortfolio.UpdateFirstInvestmentDate(updatedAssetInPortfolio.FirstInvestmentDate);

        return Result.Success;
    }

    public ErrorOr<Success> RemoveAsset(AssetInPortfolio removedAssetInPortfolio)
    {
        var existingAssetInPortfolio = _assetsInPortfolio.SingleOrDefault(a => a.Id == removedAssetInPortfolio.Id);

        if (existingAssetInPortfolio is null) return Error.NotFound(description: "Asset not found in portfolio assets");

        _assetsInPortfolio.Remove(removedAssetInPortfolio);

        return Result.Success;
    }

    public decimal TotalInvestedAmount => _assetsInPortfolio.Sum(asset => asset.InvestedAmount);

    // TODO: ENSURE ASSET'S PRICE ARE UPDATED
    public decimal TotalCurrentValue => _assetsInPortfolio.Sum(asset => asset.Quantity * asset.Asset.Price);

    public decimal PortfolioPerformanceValue => TotalCurrentValue - TotalInvestedAmount;

    public decimal PortfolioPerformancePercentage => TotalInvestedAmount == 0 ? 0 : (PortfolioPerformanceValue / TotalInvestedAmount) * 100;

    /* TODO: REVIEW
    public IDictionary<AssetInPortfolio, decimal> AllocationByAsset =>
        _assetsInPortfolio.ToDictionary(asset => asset, asset => (asset.Quantity * asset.Asset.Price) / TotalCurrentValue * 100); */

    // TODO: PORTFOLIO STATE
}