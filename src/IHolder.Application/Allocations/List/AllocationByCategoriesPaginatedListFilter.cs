using IHolder.Domain.Enumerators;
using IHolder.SharedKernel.DTO;

namespace IHolder.Application.Allocations.List;

public record AllocationByCategoriesPaginatedListFilter(
    Guid UserId,
    Guid? Id = null,
    Guid? CategoryId = null,
    string? CategoryName = null,
    string? CategoryDescription = null,
    Recommendation? Recommendation = null,
    decimal? CurrentAmount = null,
    decimal? TargetPercentage = null,
    decimal? CurrentPercentage = null,
    decimal? PercentageDifference = null,
    decimal? AmountDifference = null,
    List<Guid>? CategoryIds = null,
    int PageNumber = 1,
    short PageSize = short.MaxValue) : PaginatedFilter(PageNumber, PageSize);
