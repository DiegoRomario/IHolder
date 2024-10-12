using IHolder.Domain.Enumerators;
using IHolder.SharedKernel.DTO;

namespace IHolder.Application.Allocations.List;

public record AllocationByProductsPaginatedListFilter(
    Guid UserId,
    Guid? Id = null,
    Guid? ProductId = null,
    string? ProductName = null,
    string? ProductDescription = null,
    Recommendation? Recommendation = null,
    Risk? Risk = null,
    Guid? CategoryId = null,
    string? CategoryName = null,
    string? CategoryDescription = null,
    decimal? CurrentAmount = null,
    decimal? TargetPercentage = null,
    decimal? CurrentPercentage = null,
    decimal? PercentageDifference = null,
    decimal? AmountDifference = null,
    List<Guid>? ProductIds = null,
    int PageNumber = 1,
    short PageSize = short.MaxValue) : PaginatedFilter(PageNumber, PageSize);
