namespace IHolder.Contracts.Allocations;

public record AllocationByAssetResponse(
    Guid Id,
    Guid AssetId,
    string AssetName,
    string AssetDescription,
    string AssetTicker,
    byte Recommendation,
    decimal CurrentAmount,
    decimal TargetPercentage,
    decimal CurrentPercentage,
    decimal PercentageDifference,
    decimal AmountDifference,
    DateTime CreatedAt,
    DateTime? UpdatedAt = null);


