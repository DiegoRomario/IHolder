using IHolder.SharedKernel.DTO;

namespace IHolder.Contracts.Allocations
{
    public record AllocationByCategoryDivideTargetPercentageRequest(bool OnlyCategoriesInPortfolio) : PaginatedFilter { }
}