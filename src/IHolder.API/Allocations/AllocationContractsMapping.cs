using IHolder.Application.Allocations.Divisions;
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

    public static AllocationByCategoryDivideTargetPercentageCommand ToCommand(this AllocationByCategoryDivideTargetPercentageRequest request)
    {
        return new AllocationByCategoryDivideTargetPercentageCommand(request.OnlyCategoriesInPortfolio, request.PageNumber, request.PageSize);
    }

    public static AllocationByProductDivideTargetPercentageCommand ToCommand(this AllocationByProductDivideTargetPercentageRequest request)
    {
        return new AllocationByProductDivideTargetPercentageCommand(request.OnlyProductsInPortfolio, request.PageNumber, request.PageSize);
    }


    public static AllocationByCategoriesPaginatedListQuery ToQuery(this AllocationByCategoryPaginatedListRequest request, Guid userId)
    {
        var filter = new AllocationByCategoriesPaginatedListFilter(
                         userId,
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
                         request.CategoryIds,
                         request.PageNumber, request.PageSize);

        return new AllocationByCategoriesPaginatedListQuery(filter);
    }

    public static PaginatedList<AllocationByCategoryResponse> ToResponse(this PaginatedList<AllocationByCategory> allocation)
    {
        var items = allocation.Items.Select(c => c.ToResponse()).ToList();
        return new PaginatedList<AllocationByCategoryResponse>(items, allocation.TotalCount, allocation.PageNumber, allocation.PageSize);
    }

    public static AllocationByProductsPaginatedListQuery ToQuery(this AllocationByProductPaginatedListRequest request, Guid userId)
    {
        var filter = new AllocationByProductsPaginatedListFilter(
                         userId,
                         request.Id,
                         request.ProductId,
                         request.ProductName,
                         request.ProductDescription,
                         (Recommendation?)request.Recommendation,
                         (Risk?)request.Risk,
                         request.CategoryId,
                         request.CategoryName,
                         request.CategoryDescription,
                         request.CurrentAmount,
                         request.TargetPercentage,
                         request.CurrentPercentage,
                         request.PercentageDifference,
                         request.AmountDifference,
                         request.ProductIds,
                         request.PageNumber, request.PageSize);

        return new AllocationByProductsPaginatedListQuery(filter);
    }

    public static PaginatedList<AllocationByProductResponse> ToResponse(this PaginatedList<AllocationByProduct> allocation)
    {
        var items = allocation.Items.Select(c => c.ToResponse()).ToList();
        return new PaginatedList<AllocationByProductResponse>(items, allocation.TotalCount, allocation.PageNumber, allocation.PageSize);
    }

    public static AllocationByAssetsPaginatedListQuery ToQuery(this AllocationByAssetPaginatedListRequest request)
    {
        var filter = new AllocationByAssetsPaginatedListFilter(
                         request.Id,
                         request.AssetId,
                         request.AssetTicker,
                         request.AssetName,
                         request.AssetDescription,
                         request.AssetPrice,
                        (Recommendation?)request.Recommendation,
                         request.ProductId,
                         request.ProductName,
                         request.ProductDescription,
                         (Risk?)request.Risk,
                         request.CategoryId,
                         request.CategoryName,
                         request.CategoryDescription,
                         request.CurrentAmount,
                         request.TargetPercentage,
                         request.CurrentPercentage,
                         request.PercentageDifference,
                         request.AmountDifference,
                         request.PageNumber, request.PageSize);

        return new AllocationByAssetsPaginatedListQuery(filter);
    }

    public static PaginatedList<AllocationByAssetResponse> ToResponse(this PaginatedList<AllocationByAsset> allocation)
    {
        var items = allocation.Items.Select(c => c.ToResponse()).ToList();
        return new PaginatedList<AllocationByAssetResponse>(items, allocation.TotalCount, allocation.PageNumber, allocation.PageSize);
    }

}
