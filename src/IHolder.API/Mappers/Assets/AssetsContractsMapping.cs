using IHolder.Application.Assets.Create;
using IHolder.Application.Assets.Update;
using IHolder.Contracts.Assets;
using IHolder.Domain.Assets;

namespace IHolder.API.Mappers.Assets;

public static class AssetContractsMapping
{
    public static AssetResponse ToAssetResponse(this Asset Asset)
    {
        return new AssetResponse(
            Asset.Id,
            Asset.Description,
            Asset.Details,
            Asset.Ticker,
            Asset.Price,
            Asset.ProductId,
            Asset.Product.Description,
            Asset.Product.CategoryId,
            Asset.Product.Category.Description,
            Asset.CreatedAt,
            Asset.UpdatedAt);
    }

    public static AssetCreateCommand ToAssetCreateCommand(this AssetCreateRequest request)
    {
        return new AssetCreateCommand(request.ProductId, request.Description, request.Details, request.Ticker, request.Price);
    }

    public static AssetUpdateCommand ToAssetUpdateCommand(this AssetUpdateRequest request, Guid id)
    {
        return new AssetUpdateCommand(id, request.ProductId, request.Description, request.Details, request.Ticker, request.Price);
    }
}

