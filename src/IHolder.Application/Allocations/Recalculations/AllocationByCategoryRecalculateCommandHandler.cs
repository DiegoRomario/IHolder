﻿using ErrorOr;
using IHolder.Application.Common;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Allocations.Recalculations;
public class AllocationByCategoryRecalculateCommandHandler(
    ICategoryRepository _categoryRepository,
    ICurrentUserProvider _currentUserProvider,
    IPortfolioRepository _portfolioRepository) : IRequestHandler<AllocationByCategoryRecalculateCommand, ErrorOr<PaginatedList<AllocationByCategory>>>
{
    private readonly Guid _userID = _currentUserProvider.GetCurrentUser().Value.Id;

    public async Task<ErrorOr<PaginatedList<AllocationByCategory>>> Handle(AllocationByCategoryRecalculateCommand request, CancellationToken ct)
    {
        var allocationsByCategory = await _categoryRepository.GetAllocationsPaginatedAsync(new(UserId: _userID, PageNumber: request.PageNumber, PageSize: request.PageSize), ct);
        var investedAmount = await _portfolioRepository.GetInvestedAmount(_userID, ct);

        foreach (var item in allocationsByCategory.Items)
        {
            var investedAmountByCategory = await _portfolioRepository.GetInvestedAmountoByCategory(_userID, item.CategoryId, ct);
            item.AllocationValues.RecalculateValues(investedAmountByCategory, investedAmount);
            item.GenerateRecommendation(investedAmountByCategory, investedAmount);
            await _categoryRepository.UpdateAllocationAsync(item, ct);
        }

        allocationsByCategory = await _categoryRepository.GetAllocationsPaginatedAsync(new(UserId: _userID, PageNumber: request.PageNumber, PageSize: request.PageSize), ct);

        return allocationsByCategory;
    }
}
