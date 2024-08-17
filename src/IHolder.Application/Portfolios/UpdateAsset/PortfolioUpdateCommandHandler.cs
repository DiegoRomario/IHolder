using ErrorOr;
using IHolder.Application.Assets.Mappers;
using IHolder.Application.Common.Interfaces;
using IHolder.Application.Portfolios.Mappers;
using IHolder.Domain.Portfolios;
using MediatR;

namespace IHolder.Application.Portfolios.UpdateAsset;

public class PortfolioUpdateAssetCommandHandler(IPortfolioRepository _repository) : IRequestHandler<PortfolioUpdateAssetCommand, ErrorOr<AssetInPortfolio>>
{
    public async Task<ErrorOr<AssetInPortfolio>> Handle(PortfolioUpdateAssetCommand request, CancellationToken ct)
    {
        if (await _repository.ExistsAssetByPredicateAsync(a => a.PortfolioId == request.PortfolioId && a.Id == request.Id, ct) is false)
            return Error.NotFound(description: "Asset in potfolio not found");

        await _repository.UpdateAssetAsync(request.ToEntity(), ct);

        var assetInPortfolio = await _repository.GetAssetByIdAsync(request.Id, ct);

        if (assetInPortfolio is null)
            return Error.Conflict(description: "Failed to retrieve the updated Asset in portfolio.");

        return assetInPortfolio;
    }
}
