using IHolder.Application.Assets.Create;
using IHolder.Application.Assets.Update;
using IHolder.Domain.Assets;

namespace IHolder.Application.Assets.Mappers;

public static class AssetCommandsMapping
{
    public static Asset ToEntity(this AssetCreateCommand command)
    {
        return new Asset(command.ProductId, command.Name, command.Description, command.Ticker, command.Price);
    }

    public static Asset ToEntity(this AssetUpdateCommand command)
    {
        return new Asset(command.ProductId, command.Name, command.Description, command.Ticker, command.Price, id: command.Id);
    }
}
