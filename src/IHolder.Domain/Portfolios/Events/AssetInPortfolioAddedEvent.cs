using ErrorOr;
using IHolder.Domain.Common;

namespace IHolder.Domain.Portfolios.Events;

public record AssetInPortfolioAddedEvent(AssetInPortfolio AssetInPortfolio) : IDomainEvent
{
    public static readonly Error AssetInPortfolioNotFound = EventualConsistencyError.From(
    code: "AssetInPortfolioAddedEvent.AssetInPortfolioNotFound",
    description: "Asset in portfolio not found");
}