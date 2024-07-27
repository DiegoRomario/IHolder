using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Assets;
using MediatR;

namespace IHolder.Application.Assets.List;

public class AssetListByIdQueryHandler(IAssetRepository _repository) : IRequestHandler<AssetGetByIdQuery, ErrorOr<Asset?>>
{
    public async Task<ErrorOr<Asset?>> Handle(AssetGetByIdQuery request, CancellationToken cancellationToken)
    {
        var Asset = await _repository.GetByIdAsync(request.Id);

        if (Asset is null) return Error.NotFound(description: "Asset not found");

        return Asset;
    }
}
