using ErrorOr;
using IHolder.Application.Assets.Mappers;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Assets;
using MediatR;

namespace IHolder.Application.Assets.Create;

public class AssetCreateCommandHandler(IAssetRepository _repository) : IRequestHandler<AssetCreateCommand, ErrorOr<Asset>>
{
    public async Task<ErrorOr<Asset>> Handle(AssetCreateCommand request, CancellationToken ct)
    {
        var asset = request.ToEntity();

        await _repository.AddAsync(asset, ct);

        asset = await _repository.GetByIdAsync(asset.Id, ct);

        if (asset == null) return Error.Conflict(description: "Failed to retrieve the created Asset.");

        return asset;
    }
}
