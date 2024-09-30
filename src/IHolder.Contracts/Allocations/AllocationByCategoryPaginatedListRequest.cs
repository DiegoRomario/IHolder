using IHolder.SharedKernel.DTO;

namespace IHolder.Contracts.Allocations;

public record AllocationByCategoryPaginatedListRequest(
    Guid? Id,
    Guid? CategoryId,
    string? CategoryName,
    string? CategoryDescription,
    byte? Recommendation,
    decimal? CurrentAmount,
    decimal? TargetPercentage,
    decimal? CurrentPercentage,
    decimal? PercentageDifference,
    decimal? AmountDifference) : PaginatedFilter
{ }

