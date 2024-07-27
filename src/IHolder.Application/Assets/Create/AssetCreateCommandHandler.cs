using ErrorOr;
using IHolder.Application.Assets.Mappers;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Assets;
using MediatR;

namespace IHolder.Application.Assets.Create;

public class AssetCreateCommandHandler(IAssetRepository _repository) : IRequestHandler<AssetCreateCommand, ErrorOr<Asset>>
{
    public async Task<ErrorOr<Asset>> Handle(AssetCreateCommand request, CancellationToken cancellationToken)
    {
        var asset = request.ToAssetEntity();

        await _repository.AddAsync(asset);

        asset = await _repository.GetByIdAsync(asset.Id);

        if (asset == null) return Error.Conflict(description: "Failed to retrieve the created Asset.");

        return asset;
    }
}
