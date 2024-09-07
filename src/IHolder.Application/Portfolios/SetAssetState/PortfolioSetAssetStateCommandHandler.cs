using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Portfolios;
using MediatR;

namespace IHolder.Application.Portfolios.SetAssetState;

public class PortfolioSetAssetStateCommandHandler(IPortfolioRepository _repository) : IRequestHandler<PortfolioSetAssetStateCommand, ErrorOr<AssetInPortfolio>>
{
    public async Task<ErrorOr<AssetInPortfolio>> Handle(PortfolioSetAssetStateCommand request, CancellationToken ct)
    {
        var portfolio = await _repository.GetByPredicateAsync(p => p.Id == request.PortfolioId, ct);

        if (portfolio is null)
            return Error.NotFound(description: "Portfolio not found");

        var assetInPortfolio = portfolio.AssetsInPortfolio.SingleOrDefault(a => a.AssetId == request.Id);

        if (assetInPortfolio is null)
            return Error.NotFound(description: "Asset in portfolio not found");

        assetInPortfolio.SetState(request.State);

        await _repository.UpdateAsync(portfolio, ct);

        assetInPortfolio = await _repository.GetAssetInPortfolioByIdAsync(request.Id, ct);

        if (assetInPortfolio is null)
            return Error.Conflict(description: "Failed to retrieve the updated Asset in portfolio.");

        return assetInPortfolio;
    }
}
