using IHolder.Application.Portfolios.AddAsset;
using IHolder.Application.Portfolios.Update;
using IHolder.Application.Portfolios.UpdateAsset;
using IHolder.Contracts.Portfolios;
using IHolder.Domain.Allocations;
using IHolder.Domain.Portfolios;

namespace IHolder.API.Portfolios;

public static class PortfolioContractsMapping
{
    public static PortfolioResponse ToResponse(this Portfolio Portfolio)
    {
        return new PortfolioResponse(
            Portfolio.Id,
            Portfolio.Name,
            Portfolio.UserId,
            Portfolio.User.FirstName,
            Portfolio.User.LastName,
            Portfolio.AssetsInPortfolio.ToResponse(),
            Portfolio.AllocationsByCategory.ToResponse(),
            Portfolio.AllocationsByProduct.ToResponse(),
            Portfolio.AllocationsByAsset.ToResponse(),
            Portfolio.TotalInvestedAmount,
            Portfolio.TotalCurrentValue,
            Portfolio.PortfolioPerformanceValue,
            Portfolio.PortfolioPerformancePercentage,
            Portfolio.CreatedAt,
            Portfolio.UpdatedAt);
    }

    public static List<AllocationByCategoryResponse> ToResponse(this IEnumerable<AllocationByCategory> allocations)
    {
        return allocations.Select(allocation => new AllocationByCategoryResponse
        {
            Id = allocation.Id,
            AmountDifference = allocation.AllocationValues.AmountDifference,
            CurrentAmount = allocation.AllocationValues.CurrentAmount,
            CurrentPercentage = allocation.AllocationValues.CurrentPercentage,
            PercentageDifference = allocation.AllocationValues.PercentageDifference,
            TargetPercentage = allocation.AllocationValues.TargetPercentage,
            Recommendation = allocation.Recommendation.ToString(),
            CreatedAt = allocation.CreatedAt,
            UpdatedAt = allocation.UpdatedAt,
            CategoryName = allocation.Category.Name
        }).ToList();
    }

    public static List<AllocationByProductResponse> ToResponse(this IEnumerable<AllocationByProduct> allocations)
    {
        return allocations.Select(allocation => new AllocationByProductResponse
        {
            Id = allocation.Id,
            AmountDifference = allocation.AllocationValues.AmountDifference,
            CurrentAmount = allocation.AllocationValues.CurrentAmount,
            CurrentPercentage = allocation.AllocationValues.CurrentPercentage,
            PercentageDifference = allocation.AllocationValues.PercentageDifference,
            TargetPercentage = allocation.AllocationValues.TargetPercentage,
            Recommendation = allocation.Recommendation.ToString(),
            CreatedAt = allocation.CreatedAt,
            UpdatedAt = allocation.UpdatedAt,
            ProductName = allocation.Product.Name
        }).ToList();
    }

    public static List<AllocationByAssetResponse> ToResponse(this IEnumerable<AllocationByAsset> allocations)
    {
        return allocations.Select(allocation => new AllocationByAssetResponse
        {
            Id = allocation.Id,
            AmountDifference = allocation.AllocationValues.AmountDifference,
            CurrentAmount = allocation.AllocationValues.CurrentAmount,
            CurrentPercentage = allocation.AllocationValues.CurrentPercentage,
            PercentageDifference = allocation.AllocationValues.PercentageDifference,
            TargetPercentage = allocation.AllocationValues.TargetPercentage,
            Recommendation = allocation.Recommendation.ToString(),
            CreatedAt = allocation.CreatedAt,
            UpdatedAt = allocation.UpdatedAt,
            Ticker = allocation.AssetInPortfolio.Asset.Ticker
        }).ToList();
    }

    public static AssetInPortfolioResponse ToResponse(this AssetInPortfolio assetInPortfolio)
    {
        return new(
            assetInPortfolio.Id,
            assetInPortfolio.AssetId,
            assetInPortfolio.Asset.Ticker,
            assetInPortfolio.AveragePrice,
            assetInPortfolio.Quantity,
            assetInPortfolio.InvestedAmount,
            assetInPortfolio.FirstInvestmentDate,
            assetInPortfolio.State.ToString(),
            assetInPortfolio.StateSetAt,
            assetInPortfolio.CreatedAt,
            assetInPortfolio.UpdatedAt
        );
    }

    public static List<AssetInPortfolioResponse> ToResponse(this IEnumerable<AssetInPortfolio> assets)
    {
        return assets.Select(asset => asset.ToResponse()).ToList();
    }

    public static PortfolioUpdateCommand ToUpdateCommand(this PortfolioUpdateRequest request, Guid id)
    {
        return new PortfolioUpdateCommand(id, request.UserId, request.Name);
    }

    public static PortfolioAddAssetCommand ToCreateCommand(this PortfolioAddAssetRequest request, Guid portfolioId)
    {
        return new PortfolioAddAssetCommand(portfolioId, request.AssetId, request.AveragePrice, request.Quantity, request.FirstInvestmentDate);
    }

    public static PortfolioUpdateAssetCommand ToUpdateCommand(this PortfolioUpdateAssetRequest request, Guid portfolioId, Guid assetInPortfolioId)
    {
        return new PortfolioUpdateAssetCommand(assetInPortfolioId, portfolioId, request.AssetId, request.AveragePrice, request.Quantity, request.FirstInvestmentDate);
    }
}

