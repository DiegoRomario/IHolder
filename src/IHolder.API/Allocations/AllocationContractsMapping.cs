using IHolder.Application.Allocations.UpdateByAsset;
using IHolder.Application.Allocations.UpdateByCategory;
using IHolder.Application.Allocations.UpdateByProduct;
using IHolder.Contracts.Allocations;
using IHolder.Domain.Allocations;

namespace IHolder.API.Allocations;

public static class AllocationContractsMapping
{
    public static AllocationByCategoryResponse ToResponse(this AllocationByCategory allocation)
    {
        return new AllocationByCategoryResponse(
            allocation.Id,
            allocation.CategoryId,
            allocation.Category.Name,
            allocation.Category.Description,
            (byte)allocation.Recommendation,
            allocation.AllocationValues.CurrentAmount,
            allocation.AllocationValues.TargetPercentage,
            allocation.AllocationValues.CurrentPercentage,
            allocation.AllocationValues.PercentageDifference,
            allocation.AllocationValues.AmountDifference,
            allocation.CreatedAt,
            allocation.UpdatedAt);
    }

    public static AllocationByProductResponse ToResponse(this AllocationByProduct allocation)
    {
        return new AllocationByProductResponse(
            allocation.Id,
            allocation.ProductId,
            allocation.Product.Name,
            allocation.Product.Description,
            (byte)allocation.Recommendation,
            allocation.AllocationValues.CurrentAmount,
            allocation.AllocationValues.TargetPercentage,
            allocation.AllocationValues.CurrentPercentage,
            allocation.AllocationValues.PercentageDifference,
            allocation.AllocationValues.AmountDifference,
            allocation.CreatedAt,
            allocation.UpdatedAt);
    }

    public static AllocationByAssetResponse ToResponse(this AllocationByAsset allocation)
    {
        return new AllocationByAssetResponse(
            allocation.Id,
            allocation.AssetId,
            allocation.AssetInPortfolio.Asset.Name,
            allocation.AssetInPortfolio.Asset.Description,
            allocation.AssetInPortfolio.Asset.Ticker,
            (byte)allocation.Recommendation,
            allocation.AllocationValues.CurrentAmount,
            allocation.AllocationValues.TargetPercentage,
            allocation.AllocationValues.CurrentPercentage,
            allocation.AllocationValues.PercentageDifference,
            allocation.AllocationValues.AmountDifference,
            allocation.CreatedAt,
            allocation.UpdatedAt);
    }

    public static AllocationByCategoryUpdateCommand ToCommand(this AllocationByCategoryUpdateRequest request, Guid id)
    {
        return new AllocationByCategoryUpdateCommand(id, request.TargetPercentage);
    }

    public static AllocationByProductUpdateCommand ToCommand(this AllocationByProductUpdateRequest request, Guid id)
    {
        return new AllocationByProductUpdateCommand(id, request.TargetPercentage);
    }

    public static AllocationByAssetUpdateCommand ToCommand(this AllocationByAssetUpdateRequest request, Guid id)
    {
        return new AllocationByAssetUpdateCommand(id, request.TargetPercentage);
    }
}
