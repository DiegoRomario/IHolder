using ErrorOr;
using IHolder.Application.Common;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Allocations.Divisions;

public class AllocationByCategoryDivideTargetPercentageCommandHandler
    (ICategoryRepository _categoryRepository,
    IPortfolioRepository _portfolioRepository,
    ICurrentUserProvider currentUserProvider) : IRequestHandler<AllocationByCategoryDivideTargetPercentageCommand, ErrorOr<PaginatedList<AllocationByCategory>>>
{
    private const decimal MAX_PERCENTAGE = 100;
    private readonly Guid _userID = currentUserProvider.GetCurrentUser().Value.Id;

    public async Task<ErrorOr<PaginatedList<AllocationByCategory>>> Handle(AllocationByCategoryDivideTargetPercentageCommand request, CancellationToken ct)
    {
        var allocations = (await _categoryRepository.GetAllocationsPaginatedAsync(new(UserId: _userID, PageSize: short.MaxValue), ct)).Items.ToList();

        if (request.OnlyCategoriesInPortfolio)
            await UpdateAllocationByCategoryInPortfolio(allocations, ct);
        else
            await UpdateAllocationByCategoryRegistered(allocations, ct);

        var result = await _categoryRepository.GetAllocationsPaginatedAsync(new(UserId: _userID, PageNumber: request.PageNumber, PageSize: request.PageSize), ct);

        return result;
    }

    private async Task UpdateAllocationByCategoryRegistered(List<AllocationByCategory> allocations, CancellationToken ct)
    {
        decimal percentageDistribution = CalculatePercentageDistribution(allocations.Count);

        foreach (var allocation in allocations)
        {
            allocation.AllocationValues.UpdateTargetPercentage(percentageDistribution);
            await _categoryRepository.UpdateAllocationAsync(allocation, ct);
        }
    }

    private async Task UpdateAllocationByCategoryInPortfolio(List<AllocationByCategory> allocations, CancellationToken ct)
    {
        List<AllocationByCategory> allocationsInPortfolio = await GetAllocationByCategoriesInPortfolio(ct);

        decimal percentageDistribution = CalculatePercentageDistribution(allocationsInPortfolio.Count);

        foreach (var allocation in allocations)
        {
            var hasAllocationInPortfolio = allocationsInPortfolio.Where(a => a.CategoryId == allocation.CategoryId).Any();
            allocation.AllocationValues.UpdateTargetPercentage(hasAllocationInPortfolio ? percentageDistribution : 0);

            await _categoryRepository.UpdateAllocationAsync(allocation, ct);
        }
    }
    private async Task<List<AllocationByCategory>> GetAllocationByCategoriesInPortfolio(CancellationToken ct)
    {
        var categoryIDsInPortfolio = await _portfolioRepository.GetAllCategoryIdsInPortfolioByUserAsync(_userID, ct);

        var allocations = (await _categoryRepository.GetAllocationsPaginatedAsync(new(UserId: _userID, CategoryIds: categoryIDsInPortfolio), ct)).Items.ToList();
        return allocations;
    }

    private static decimal CalculatePercentageDistribution(int count) => count > 0 ? MAX_PERCENTAGE / count : 0;
}
