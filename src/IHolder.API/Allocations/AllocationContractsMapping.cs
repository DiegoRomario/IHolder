using IHolder.Application.Allocations.List;
using IHolder.Application.Allocations.UpdateByAsset;
using IHolder.Application.Allocations.UpdateByCategory;
using IHolder.Application.Allocations.UpdateByProduct;
using IHolder.Contracts.Allocations;
using IHolder.Domain.Allocations;
using IHolder.Domain.Enumerators;
using IHolder.SharedKernel.DTO;

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

    public static AllocationByCategoriesPaginatedListQuery ToQuery(this AllocationByCategoryPaginatedListRequest request)
    {
        var filter = new AllocationByCategoriesPaginatedListFilter(
                         request.Id,
                         request.CategoryId,
                         request.CategoryName,
                         request.CategoryDescription,
                         (Recommendation?)request.Recommendation,
                         request.CurrentAmount,
                         request.TargetPercentage,
                         request.CurrentPercentage,
                         request.PercentageDifference,
                         request.AmountDifference,
                         request.PageNumber, request.PageSize);

        return new AllocationByCategoriesPaginatedListQuery(filter);
    }

    public static PaginatedList<AllocationByCategoryResponse> ToResponse(this PaginatedList<AllocationByCategory> allocationByCategory)
    {
        var items = allocationByCategory.Items.Select(c => c.ToResponse()).ToList();
        return new PaginatedList<AllocationByCategoryResponse>(items, allocationByCategory.TotalCount, allocationByCategory.PageNumber, allocationByCategory.PageSize);
    }

}
