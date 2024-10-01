using IHolder.Domain.Enumerators;
using IHolder.SharedKernel.DTO;

namespace IHolder.Application.Allocations.List;

public record AllocationByProductsPaginatedListFilter(
    Guid? Id,
    Guid? ProductId,
    string? ProductName,
    string? ProductDescription,
    Recommendation? Recommendation,
    Risk? Risk,
    Guid? CategoryId,
    string? CategoryName,
    string? CategoryDescription,
    decimal? CurrentAmount,
    decimal? TargetPercentage,
    decimal? CurrentPercentage,
    decimal? PercentageDifference,
    decimal? AmountDifference,
    int PageNumber,
    short PageSize) : PaginatedFilter(PageNumber, PageSize);
