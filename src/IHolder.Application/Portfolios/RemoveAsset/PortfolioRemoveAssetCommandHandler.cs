using ErrorOr;
using IHolder.Application.Common.Interfaces;
using MediatR;

namespace IHolder.Application.Portfolios.RemoveAsset;

public class PortfolioRemoveAssetCommandHandler(IPortfolioRepository _portfolioRepository) : IRequestHandler<PortfolioRemoveAssetCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(PortfolioRemoveAssetCommand request, CancellationToken ct)
    {
        var portfolio = await _portfolioRepository.GetByIdAsync(request.PortfolioId, ct);

        if (portfolio is null) return Error.NotFound(description: "Portfolio not found");

        var assetInPortfolio = portfolio.AssetsInPortfolio.SingleOrDefault(a => a.Id == request.Id);

        if (assetInPortfolio is null)
            return Error.NotFound(description: "Asset in portfolio not found");

        var hasAllocations = await _portfolioRepository.HasAllocationsByAssetInPortfolioAsync(assetInPortfolio.Id, ct);

        if (hasAllocations)
            return Error.Conflict(description: "Unable to remove asset from portfolio. There are allocations for this asset.");

        var removeAssetResult = portfolio.RemoveAsset(assetInPortfolio);

        if (removeAssetResult.IsError) return removeAssetResult.Errors;

        await _portfolioRepository.UpdateAsync(portfolio, ct);

        return Result.Deleted;
    }
}
