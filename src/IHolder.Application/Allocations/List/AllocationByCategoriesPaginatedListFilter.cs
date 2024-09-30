using IHolder.Domain.Enumerators;
using IHolder.SharedKernel.DTO;

namespace IHolder.Application.Allocations.List;

public record AllocationByCategoriesPaginatedListFilter(
    Guid? Id,
    Guid? CategoryId,
    string? CategoryName,
    string? CategoryDescription,
    Recommendation? Recommendation,
    decimal? CurrentAmount,
    decimal? TargetPercentage,
    decimal? CurrentPercentage,
    decimal? PercentageDifference,
    decimal? AmountDifference,
    int PageNumber,
    short PageSize) : PaginatedFilter(PageNumber, PageSize);
