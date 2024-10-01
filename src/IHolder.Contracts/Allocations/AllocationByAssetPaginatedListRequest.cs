using IHolder.SharedKernel.DTO;

namespace IHolder.Contracts.Allocations;

public record AllocationByAssetPaginatedListRequest(
    Guid? Id,
    Guid? AssetId,
    string? AssetTicker,
    string? AssetName,
    string? AssetDescription,
    decimal? AssetPrice,
    byte? Recommendation,
    Guid? ProductId,
    string? ProductName,
    string? ProductDescription,
    byte? Risk,
    Guid? CategoryId,
    string? CategoryName,
    string? CategoryDescription,
    decimal? CurrentAmount,
    decimal? TargetPercentage,
    decimal? CurrentPercentage,
    decimal? PercentageDifference,
    decimal? AmountDifference) : PaginatedFilter
{ }

