using IHolder.Application.Allocations.Mappers;
using IHolder.Application.Common;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Categories.Events;
using IHolder.Domain.Common;
using MediatR;

namespace IHolder.Application.Allocations.Events;

internal class CategoryCreatedEventHandler(ICategoryRepository _categoryRepository, IPortfolioRepository _portfolioRepository, ICurrentUserProvider currentUserProvider) : INotificationHandler<CategoryCreatedEvent>
{
    public async Task Handle(CategoryCreatedEvent categoryCreatedEvent, CancellationToken ct)
    {
        var portfolio = await _portfolioRepository.GetByPredicateAsync(p => p.UserId == currentUserProvider.GetCurrentUser().Value.Id, ct);

        if (portfolio is null)
            throw new EventualConsistencyException(CategoryCreatedEvent.PortfolioNotFound, null);

        var allocation = categoryCreatedEvent.ToEntity(portfolio!.Id);

        await _categoryRepository.AddAllocationAsync(allocation, ct);
    }
}
