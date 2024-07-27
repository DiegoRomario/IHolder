using IHolder.Application.Assets.Create;
using IHolder.Application.Assets.Update;
using IHolder.Domain.Assets;

namespace IHolder.Application.Assets.Mappers;

public static class AssetCommandsMapping
{
    public static Asset ToAssetEntity(this AssetCreateCommand command)
    {
        return new Asset(command.ProductId, command.Description, command.Details, command.Ticker, command.Price);
    }

    public static Asset ToAssetEntity(this AssetUpdateCommand command)
    {
        return new Asset(command.ProductId, command.Description, command.Details, command.Ticker, command.Price, id: command.Id);
    }
}
