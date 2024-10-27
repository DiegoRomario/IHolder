using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Allocations;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Allocations.Recalculations;
public class AllocationByProductRecalculateCommandHandler(
    IProductRepository _productRepository,
    ICurrentUserProvider _currentUserProvider,
    IPortfolioRepository _portfolioRepository) : IRequestHandler<AllocationByProductRecalculateCommand, ErrorOr<PaginatedList<AllocationByProduct>>>
{
    private readonly Guid _userID = _currentUserProvider.GetCurrentUser().Value.Id;

    public async Task<ErrorOr<PaginatedList<AllocationByProduct>>> Handle(AllocationByProductRecalculateCommand request, CancellationToken ct)
    {
        var allocationsByProduct = await _productRepository.GetAllocationsPaginatedAsync(new(UserId: _userID, PageNumber: request.PageNumber, PageSize: request.PageSize), ct);
        var investedAmount = await _portfolioRepository.GetInvestedAmount(_userID, ct);

        foreach (var item in allocationsByProduct.Items)
        {
            var investedAmountByProduct = await _portfolioRepository.GetInvestedAmountoByProduct(_userID, item.ProductId, ct);
            item.AllocationValues.RecalculateValues(investedAmountByProduct, investedAmount);
            item.GenerateRecommendation(investedAmountByProduct, investedAmount);
            await _productRepository.UpdateAllocationAsync(item, ct);
        }

        allocationsByProduct = await _productRepository.GetAllocationsPaginatedAsync(new(UserId: _userID, PageNumber: request.PageNumber, PageSize: request.PageSize), ct);

        return allocationsByProduct;
    }
}
