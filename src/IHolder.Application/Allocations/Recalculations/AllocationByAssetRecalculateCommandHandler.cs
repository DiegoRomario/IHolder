﻿using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Allocations.Recalculations;
public class AllocationByAssetRecalculateCommandHandler(ICurrentUserProvider _currentUserProvider, IPortfolioRepository _portfolioRepository)
    : IRequestHandler<AllocationByAssetRecalculateCommand, ErrorOr<PaginatedList<AllocationByAsset>>>
{
    private readonly Guid _userID = _currentUserProvider.GetCurrentUser().Value.Id;

    public async Task<ErrorOr<PaginatedList<AllocationByAsset>>> Handle(AllocationByAssetRecalculateCommand request, CancellationToken ct)
    {
        var allocationsByAsset = await _portfolioRepository.GetAllocationsPaginatedAsync(new(UserId: _userID, PageNumber: request.PageNumber, PageSize: request.PageSize), ct);
        var investedAmount = await _portfolioRepository.GetInvestedAmount(_userID, ct);

        foreach (var item in allocationsByAsset.Items)
        {
            var investedAmountByAsset = await _portfolioRepository.GetInvestedAmountoByAsset(_userID, item.AssetInPortfolioId, ct);
            item.AllocationValues.RecalculateValues(investedAmountByAsset, investedAmount);
            item.GenerateRecommendation(investedAmountByAsset, investedAmount);
            await _portfolioRepository.UpdateAllocationAsync(item, ct);
        }

        allocationsByAsset = await _portfolioRepository.GetAllocationsPaginatedAsync(new(UserId: _userID, PageNumber: request.PageNumber, PageSize: request.PageSize), ct);

        return allocationsByAsset;
    }
}
