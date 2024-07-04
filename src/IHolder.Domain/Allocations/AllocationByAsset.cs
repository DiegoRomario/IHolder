using IHolder.Domain.Assets;
using IHolder.Domain.Enumerators;

namespace IHolder.Domain.Allocations;

public class AllocationByAsset : Allocation
{
    private const int MAXIMUM_DAYS_IN_QUARANTINE = 180;
    private AllocationByAsset() { }

    // TODO: do I need a better design for this class? should asset be received in the contructor?
    public AllocationByAsset(Asset asset, Guid userId, decimal targetPercentage) : base(targetPercentage, userId)
    {
        Asset = asset;
        AssetId = asset.Id;
    }

    public Guid AssetId { get; private set; }
    public Asset Asset { get; private set; } = default!;

    protected override Recommendation ProcessRecommendation()
    {
        if (IsQuarantineTimeExceeded() || ExceedsPercentageDifference())
            return Recommendation.Sell;
        else if (AllocationValues.PercentageDifference > 0 && Asset.State != State.Quarantine)
            return Recommendation.Buy;

        return Recommendation.Hold;
    }

    private bool IsQuarantineTimeExceeded() => Asset.State == State.Quarantine && Asset.StateSetAt.AddDays(MAXIMUM_DAYS_IN_QUARANTINE) < DateTime.Now;
}
