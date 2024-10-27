using IHolder.Application.Allocations.Mappers;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Common;
using IHolder.Domain.Portfolios.Events;
using MediatR;

namespace IHolder.Application.Allocations.Events;

internal class AssetInPortfolioAddedEventHandler(IPortfolioRepository _portfolioRepository) : INotificationHandler<AssetInPortfolioAddedEvent>
{
    public async Task Handle(AssetInPortfolioAddedEvent assetCreatedEvent, CancellationToken ct)
    {
        var assetInPortfolio = await _portfolioRepository.GetAssetInPortfolioByIdAsync(assetCreatedEvent.AssetInPortfolio.Id, ct);

        if (assetInPortfolio is null) throw new EventualConsistencyException(AssetInPortfolioAddedEvent.AssetInPortfolioNotFound, null);

        var allocation = assetCreatedEvent.ToEntity();
        await _portfolioRepository.AddAllocationAsync(allocation, ct);
    }
}
