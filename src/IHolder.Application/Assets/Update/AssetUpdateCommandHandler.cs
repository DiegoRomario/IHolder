using ErrorOr;
using IHolder.Application.Assets.Mappers;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Assets;
using MediatR;

namespace IHolder.Application.Assets.Update;

public class AssetUpdateCommandHandler(IAssetRepository _repository) : IRequestHandler<AssetUpdateCommand, ErrorOr<Asset>>
{
    public async Task<ErrorOr<Asset>> Handle(AssetUpdateCommand request, CancellationToken cancellationToken)
    {
        if (await _repository.ExistsByPredicateAsync(a => a.Id == request.Id) is false)
            return Error.Conflict(description: "Asset not found");

        await _repository.UpdateAsync(request.ToAssetEntity());

        var Asset = await _repository.GetByIdAsync(request.Id);

        if (Asset == null) return Error.Conflict(description: "Failed to retrieve the updated Asset.");

        return Asset;
    }
}
