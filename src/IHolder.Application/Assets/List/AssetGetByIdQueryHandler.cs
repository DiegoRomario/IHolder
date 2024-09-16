using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Assets;
using MediatR;

namespace IHolder.Application.Assets.List;

public class AssetGetByIdQueryHandler(IAssetRepository _repository) : IRequestHandler<AssetGetByIdQuery, ErrorOr<Asset?>>
{
    public async Task<ErrorOr<Asset?>> Handle(AssetGetByIdQuery request, CancellationToken ct)
    {
        var Asset = await _repository.GetByIdAsync(request.Id, ct);

        if (Asset is null) return Error.NotFound(description: "Asset not found");

        return Asset;
    }
}
