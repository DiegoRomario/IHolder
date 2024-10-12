using IHolder.SharedKernel.DTO;

namespace IHolder.Contracts.Allocations
{
    public record AllocationByProductDivideTargetPercentageRequest(bool OnlyProductsInPortfolio) : PaginatedFilter { }
}