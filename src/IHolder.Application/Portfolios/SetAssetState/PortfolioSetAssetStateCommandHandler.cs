using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Portfolios;
using MediatR;

namespace IHolder.Application.Portfolios.SetAssetState;

public class PortfolioSetAssetStateCommandHandler(IPortfolioRepository _repository) : IRequestHandler<PortfolioSetAssetStateCommand, ErrorOr<AssetInPortfolio>>
{
    public async Task<ErrorOr<AssetInPortfolio>> Handle(PortfolioSetAssetStateCommand request, CancellationToken ct)
    {
        var assetInPortfolio = await _repository.GetAssetByIdAsync(request.Id, ct);

        if (assetInPortfolio is null)
            return Error.NotFound(description: "Asset in potfolio not found");

        assetInPortfolio.SetState(request.State);

        await _repository.UpdateAssetAsync(assetInPortfolio, ct);

        assetInPortfolio = await _repository.GetAssetByIdAsync(request.Id, ct);

        if (assetInPortfolio is null)
            return Error.Conflict(description: "Failed to retrieve the updated Asset in portfolio.");

        return assetInPortfolio;
    }
}
