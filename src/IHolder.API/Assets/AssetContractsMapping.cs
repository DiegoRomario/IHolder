using IHolder.Application.Assets.Create;
using IHolder.Application.Assets.List;
using IHolder.Application.Assets.Update;
using IHolder.Contracts.Assets;
using IHolder.Domain.Assets;
using IHolder.SharedKernel.DTO;

namespace IHolder.API.Assets;

public static class AssetContractsMapping
{
    public static AssetResponse ToResponse(this Asset Asset)
    {
        return new AssetResponse(
            Asset.Id,
            Asset.Name,
            Asset.Description,
            Asset.Ticker,
            Asset.Price,
            Asset.ProductId,
            Asset.Product.Description,
            Asset.Product.CategoryId,
            Asset.Product.Category.Description,
            Asset.CreatedAt,
            Asset.UpdatedAt);
    }

    public static AssetCreateCommand ToCommand(this AssetCreateRequest request)
    {
        return new AssetCreateCommand(request.ProductId, request.Name, request.Description, request.Ticker, request.Price);
    }

    public static AssetUpdateCommand ToCommand(this AssetUpdateRequest request, Guid id)
    {
        return new AssetUpdateCommand(id, request.ProductId, request.Name, request.Description, request.Ticker, request.Price);
    }

    public static AssetsPaginatedListQuery ToQuery(this AssetPaginatedListRequest request)
    {
        return new AssetsPaginatedListQuery(new AssetsPaginatedListFilter(
            request.Id,
            request.Name,
            request.Description,
            request.Ticker,
            request.MinPrice,
            request.MaxPrice,
            request.ProductId,
            request.ProductName,
            request.CategoryId,
            request.CategoryName,
            request.PageNumber,
            request.PageSize));
    }

    public static PaginatedList<AssetResponse> ToResponse(this PaginatedList<Asset> asset)
    {
        var items = asset.Items.Select(c => c.ToResponse()).ToList();
        return new PaginatedList<AssetResponse>(items, asset.TotalCount, asset.PageNumber, asset.PageSize);
    }

    public static AssetQuoteResponse ToResponse(this AssetQuoteDTO assetQuote)
    {
        return new AssetQuoteResponse(assetQuote.PreviousQuote, assetQuote.Quote, assetQuote.Variation, assetQuote.PercentageVariation);
    }
}

