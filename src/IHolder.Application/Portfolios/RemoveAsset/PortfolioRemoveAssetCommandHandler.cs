using ErrorOr;
using IHolder.Application.Common.Interfaces;
using MediatR;

namespace IHolder.Application.Portfolios.AddAsset;

public class PortfolioRemoveAssetCommandHandler(IPortfolioRepository _portfolioRepository, IAssetRepository _assetRepository) : IRequestHandler<PortfolioRemoveAssetCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(PortfolioRemoveAssetCommand request, CancellationToken ct)
    {
        var assetInPortfolio = await _portfolioRepository.GetAssetByIdAsync(request.Id, ct);

        if (assetInPortfolio is null)
            return Error.NotFound(description: "Asset in portfolio not found");

        var hasAllocations = await _assetRepository.HasAllocationsAsync(assetInPortfolio.Id, ct);

        if (hasAllocations)
            return Error.Conflict(description: "Unable to remove asset from portfolio. There are allocations for this asset.");

        await _portfolioRepository.RemoveAssetAsync(assetInPortfolio, ct);

        return Result.Deleted;
    }
}
