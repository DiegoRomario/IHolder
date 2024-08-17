using ErrorOr;
using IHolder.Application.Assets.Mappers;
using IHolder.Application.Common.Interfaces;
using IHolder.Application.Portfolios.Mappers;
using IHolder.Domain.Portfolios;
using MediatR;

namespace IHolder.Application.Portfolios.AddAsset;

public class PortfolioAddAssetCommandHandler(IPortfolioRepository _portfolioRepository) : IRequestHandler<PortfolioAddAssetCommand, ErrorOr<AssetInPortfolio>>
{
    public async Task<ErrorOr<AssetInPortfolio>> Handle(PortfolioAddAssetCommand request, CancellationToken ct)
    {
        if (await _portfolioRepository.ExistsByPredicateAsync(a => a.Id == request.PortfolioId, ct) is false)
            return Error.NotFound(description: "Portfolio not found");

        var assetInPortfolio = request.ToEntity();

        await _portfolioRepository.AddAssetAsync(assetInPortfolio, ct);

        assetInPortfolio = await _portfolioRepository.GetAssetByIdAsync(assetInPortfolio.Id, ct);

        if (assetInPortfolio == null) return Error.Conflict(description: "Failed to retrieve the created Portfolio.");

        return assetInPortfolio;
    }
}
