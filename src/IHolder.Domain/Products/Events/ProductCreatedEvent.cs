using ErrorOr;
using IHolder.Domain.Common;

namespace IHolder.Domain.Products.Events;

public record ProductCreatedEvent(Guid ProductId) : IDomainEvent
{
    public static readonly Error PortfolioNotFound = EventualConsistencyError.From(
    code: "ProductCreatedEvent.PortfolioNotFound",
    description: "Portfolio not found");
}