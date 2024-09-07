using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Application.Portfolios.Mappers;
using IHolder.Domain.Portfolios;
using MediatR;

namespace IHolder.Application.Portfolios.UpdateAsset;

public class PortfolioUpdateAssetCommandHandler(IPortfolioRepository _repository) : IRequestHandler<PortfolioUpdateAssetCommand, ErrorOr<AssetInPortfolio>>
{
    public async Task<ErrorOr<AssetInPortfolio>> Handle(PortfolioUpdateAssetCommand request, CancellationToken ct)
    {
        var portfolio = await _repository.GetByIdAsync(request.PortfolioId, ct);

        if (portfolio is null)
            return Error.NotFound(description: "Asset in potfolio not found");

        var assetInPortfolio = portfolio.AssetsInPortfolio.SingleOrDefault(a => a.Id == request.Id);

        if (assetInPortfolio is null)
            return Error.NotFound(description: "Asset in potfolio not found");

        var updateAssetResult = portfolio.UpdateAsset(request.ToEntity(assetInPortfolio.AssetId));

        if (updateAssetResult.IsError) return updateAssetResult.Errors;

        await _repository.UpdateAsync(portfolio, ct);

        assetInPortfolio = await _repository.GetAssetInPortfolioByIdAsync(request.Id, ct);

        if (assetInPortfolio is null)
            return Error.Conflict(description: "Failed to retrieve the updated Asset in portfolio.");

        return assetInPortfolio;
    }
}
