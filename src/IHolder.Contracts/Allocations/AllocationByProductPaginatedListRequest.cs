using IHolder.SharedKernel.DTO;

namespace IHolder.Contracts.Allocations;

public record AllocationByProductPaginatedListRequest(
    Guid? Id,
    Guid? ProductId,
    string? ProductName,
    string? ProductDescription,
    byte? Recommendation,
    byte? Risk,
    Guid? CategoryId,
    string? CategoryName,
    string? CategoryDescription,
    decimal? CurrentAmount,
    decimal? TargetPercentage,
    decimal? CurrentPercentage,
    decimal? PercentageDifference,
    decimal? AmountDifference,
    List<Guid>? ProductIds) : PaginatedFilter
{ }

