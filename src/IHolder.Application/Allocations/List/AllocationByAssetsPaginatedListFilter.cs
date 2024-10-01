using IHolder.Domain.Enumerators;
using IHolder.SharedKernel.DTO;

namespace IHolder.Application.Allocations.List;

public record AllocationByAssetsPaginatedListFilter(
    Guid? Id,
    Guid? AssetId,
    string? AssetTicker,
    string? AssetName,
    string? AssetDescription,
    decimal? AssetPrice,
    Recommendation? Recommendation,
    Guid? ProductId,
    string? ProductName,
    string? ProductDescription,
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
