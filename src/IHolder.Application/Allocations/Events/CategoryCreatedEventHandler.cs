using IHolder.Application.Allocations.Mappers;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Categories.Events;
using MediatR;

namespace IHolder.Application.Allocations.Events;

internal class CategoryCreatedEventHandler(IAllocationRepository _allocationRepository, IPortfolioRepository _portfolioRepository, ICurrentUserProvider currentUserProvider) : INotificationHandler<CategoryCreatedEvent>
{
    public async Task Handle(CategoryCreatedEvent categoryCreatedEvent, CancellationToken ct)
    {
        var portfolio = await _portfolioRepository.GetByUserIdAsync(currentUserProvider.GetCurrentUser().Value.Id, ct);

        // TODO: EventualConsistencyException
        // if (portfolio == null)

        var allocation = categoryCreatedEvent.ToEntity(portfolio!.Id);
        await _allocationRepository.AddAsync(allocation, ct);
    }
}