using ErrorOr;
using IHolder.Application.Common.Interfaces;
using MediatR;

namespace IHolder.Application.Assets.Delete;

public class AssetDeleteCommandHandler(IAssetRepository _repository, IPortfolioRepository _portfolioRepository) : IRequestHandler<AssetDeleteCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(AssetDeleteCommand request, CancellationToken ct)
    {
        var asset = await _repository.GetByIdAsync(request.Id, ct);

        if (asset is null) return Error.NotFound(description: "Asset not found");

        var assetInPortfolioExists = await _portfolioRepository.ExistsAssetInPortfolioByPredicateAsync(p => p.AssetId == request.Id, ct);

        if (assetInPortfolioExists) return Error.Conflict(description: "Unable to delete asset. This asset is in the user's portfolio.");

        await _repository.DeleteAsync(asset, ct);

        return Result.Deleted;
    }
}
