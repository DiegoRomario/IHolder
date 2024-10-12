using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Allocations.Divisions;

public class AllocationByAssetDivideTargetPercentageCommandHandler
    (IAllocationByAssetRepository _allocationByAssetRepository,
    IPortfolioRepository _portfolioRepository,
    ICurrentUserProvider currentUserProvider) : IRequestHandler<AllocationByAssetDivideTargetPercentageCommand, ErrorOr<PaginatedList<AllocationByAsset>>>
{
    private const decimal MAX_PERCENTAGE = 100;
    private readonly Guid _userID = currentUserProvider.GetCurrentUser().Value.Id;

    public async Task<ErrorOr<PaginatedList<AllocationByAsset>>> Handle(AllocationByAssetDivideTargetPercentageCommand request, CancellationToken ct)
    {
        var allocations = (await _allocationByAssetRepository.GetPaginatedAsync(new(UserId: _userID, PageSize: short.MaxValue), ct)).Items.ToList();

        await UpdateAllocationByAssetInPortfolio(allocations, ct);

        var result = await _allocationByAssetRepository.GetPaginatedAsync(new(UserId: _userID, PageNumber: request.PageNumber, PageSize: request.PageSize), ct);

        return result;
    }

    private async Task UpdateAllocationByAssetInPortfolio(List<AllocationByAsset> allocations, CancellationToken ct)
    {
        List<AllocationByAsset> allocationsInPortfolio = await GetAllocationByAssetsInPortfolio(ct);

        decimal percentageDistribution = CalculatePercentageDistribution(allocationsInPortfolio.Count);

        foreach (var allocation in allocations)
        {
            var hasAllocationInPortfolio = allocationsInPortfolio.Where(a => a.AssetId == allocation.AssetId).Any();
            allocation.AllocationValues.UpdateTargetPercentage(hasAllocationInPortfolio ? percentageDistribution : 0);

            await _allocationByAssetRepository.UpdateAsync(allocation, ct);
        }
    }

    private async Task<List<AllocationByAsset>> GetAllocationByAssetsInPortfolio(CancellationToken ct)
    {
        var assetIDsInPortfolio = await _portfolioRepository.GetAllAssetIdsInPortfolioByUserAsync(_userID, ct);

        var allocations = (await _allocationByAssetRepository.GetPaginatedAsync(new(UserId: _userID, AssetIds: assetIDsInPortfolio), ct)).Items.ToList();
        return allocations;
    }

    private static decimal CalculatePercentageDistribution(int count) => count > 0 ? MAX_PERCENTAGE / count : 0;
}
