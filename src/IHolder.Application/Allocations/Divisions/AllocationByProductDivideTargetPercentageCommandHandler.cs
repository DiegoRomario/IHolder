using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Allocations.Divisions;

public class AllocationByProductDivideTargetPercentageCommandHandler
    (IAllocationByProductRepository _allocationByProductRepository,
    IPortfolioRepository _portfolioRepository,
    ICurrentUserProvider currentUserProvider) : IRequestHandler<AllocationByProductDivideTargetPercentageCommand, ErrorOr<PaginatedList<AllocationByProduct>>>
{
    private const decimal MAX_PERCENTAGE = 100;
    private readonly Guid _userID = currentUserProvider.GetCurrentUser().Value.Id;

    public async Task<ErrorOr<PaginatedList<AllocationByProduct>>> Handle(AllocationByProductDivideTargetPercentageCommand request, CancellationToken ct)
    {
        var allocations = (await _allocationByProductRepository.GetPaginatedAsync(new(UserId: _userID, PageSize: short.MaxValue), ct)).Items.ToList();

        if (request.OnlyProductsInPortfolio)
            await UpdateAllocationByProductInPortfolio(allocations, ct);
        else
            await UpdateAllocationByProductRegistered(allocations, ct);

        var result = await _allocationByProductRepository.GetPaginatedAsync(new(UserId: _userID, PageNumber: request.PageNumber, PageSize: request.PageSize), ct);

        return result;
    }

    private async Task UpdateAllocationByProductRegistered(List<AllocationByProduct> allocations, CancellationToken ct)
    {
        decimal percentageDistribution = CalculatePercentageDistribution(allocations.Count);

        foreach (var allocation in allocations)
        {
            allocation.AllocationValues.UpdateTargetPercentage(percentageDistribution);
            await _allocationByProductRepository.UpdateAsync(allocation, ct);
        }
    }

    private async Task UpdateAllocationByProductInPortfolio(List<AllocationByProduct> allocations, CancellationToken ct)
    {
        List<AllocationByProduct> allocationsInPortfolio = await GetAllocationByProductsInPortfolio(ct);

        decimal percentageDistribution = CalculatePercentageDistribution(allocationsInPortfolio.Count);

        foreach (var allocation in allocations)
        {
            var hasAllocationInPortfolio = allocationsInPortfolio.Where(a => a.ProductId == allocation.ProductId).Any();
            allocation.AllocationValues.UpdateTargetPercentage(hasAllocationInPortfolio ? percentageDistribution : 0);

            await _allocationByProductRepository.UpdateAsync(allocation, ct);
        }
    }

    private async Task<List<AllocationByProduct>> GetAllocationByProductsInPortfolio(CancellationToken ct)
    {
        var productIDsInPortfolio = await _portfolioRepository.GetAllProductIdsInPortfolioByUserAsync(_userID, ct);

        var allocations = (await _allocationByProductRepository.GetPaginatedAsync(new(UserId: _userID, ProductIds: productIDsInPortfolio), ct)).Items.ToList();
        return allocations;
    }

    private static decimal CalculatePercentageDistribution(int count) => count > 0 ? MAX_PERCENTAGE / count : 0;
}
