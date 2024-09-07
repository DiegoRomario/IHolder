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
        var portfolio = await _portfolioRepository.GetByIdAsync(request.PortfolioId, ct);

        if (portfolio is null) return Error.NotFound(description: "Portfolio not found");

        var assetInPortfolio = request.ToEntity();

        var addAssetResult = portfolio.AddAsset(assetInPortfolio);

        if (addAssetResult.IsError) return addAssetResult.Errors;

        await _portfolioRepository.UpdateAsync(portfolio, ct);

        assetInPortfolio = await _portfolioRepository.GetAssetInPortfolioByIdAsync(assetInPortfolio.Id, ct);

        if (assetInPortfolio == null) return Error.Conflict(description: "Failed to retrieve the created Portfolio.");

        return assetInPortfolio;
    }
}
