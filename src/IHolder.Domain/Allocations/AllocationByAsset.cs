using IHolder.Domain.Assets;
using IHolder.Domain.Enumerators;

namespace IHolder.Domain.Allocations;

public class AllocationByAsset : Allocation
{
    private const int MAXIMUM_DAYS_IN_QUARANTINE = 180;
    private AllocationByAsset() { }
    public AllocationByAsset(Asset asset, Guid userId, decimal targetPercentage) : base(targetPercentage, userId)
    {
        Asset = asset;
    }

    public Asset Asset { get; private set; } = default!;

    protected override Recommendation ProcessRecommendation()
    {
        if ((IsQuarantineTimeExceeded() && Asset.State == State.Quarantine) || ExceedsPercentageDifference())
            return Recommendation.Sell;
        else if (AllocationValues.PercentageDifference > 0 && Asset.State != State.Quarantine)
            return Recommendation.Buy;

        return Recommendation.Hold;
    }

    private bool IsQuarantineTimeExceeded()
    {
        return Asset.StateSetAt.AddDays(MAXIMUM_DAYS_IN_QUARANTINE) < DateTime.Now;
    }
}
