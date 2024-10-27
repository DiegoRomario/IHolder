using IHolder.Application.Allocations.Mappers;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Common;
using IHolder.Domain.Products.Events;
using MediatR;

namespace IHolder.Application.Allocations.Events;

internal class ProductCreatedEventHandler(IProductRepository _productRepository, IPortfolioRepository _portfolioRepository, ICurrentUserProvider currentUserProvider) : INotificationHandler<ProductCreatedEvent>
{
    public async Task Handle(ProductCreatedEvent productCreatedEvent, CancellationToken ct)
    {
        var portfolio = await _portfolioRepository.GetByPredicateAsync(p => p.UserId == currentUserProvider.GetCurrentUser().Value.Id, ct);

        if (portfolio is null) throw new EventualConsistencyException(ProductCreatedEvent.PortfolioNotFound, null);

        var allocation = productCreatedEvent.ToEntity(portfolio!.Id);
        await _productRepository.AddAllocationAsync(allocation, ct);
    }
}
