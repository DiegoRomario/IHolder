using IHolder.Domain.Enumerators;
using IHolder.Domain.Portfolios;

namespace IHolder.Domain.Allocations;

public class AllocationByAsset : Allocation
{
    private const int MAXIMUM_DAYS_IN_QUARANTINE = 180;
    private AllocationByAsset() { }

    // TODO: do I need a better design for this class? should asset in portfolio be received in the contructor?
    public AllocationByAsset(AssetInPortfolio assetInPortfolio, decimal targetPercentage) : base(targetPercentage, assetInPortfolio.PortfolioId)
    {
        AssetInPortfolio = assetInPortfolio;
        AssetInPortfolioId = assetInPortfolio.Id;
    }

    public Guid AssetInPortfolioId { get; private set; }
    public AssetInPortfolio AssetInPortfolio { get; private set; } = default!;

    protected override Recommendation ProcessRecommendation()
    {
        if (IsQuarantineTimeExceeded() || ExceedsPercentageDifference())
            return Recommendation.Sell;
        else if (AllocationValues.PercentageDifference > 0 && AssetInPortfolio.State != State.Quarantine)
            return Recommendation.Buy;

        return Recommendation.Hold;
    }

    private bool IsQuarantineTimeExceeded()
            => AssetInPortfolio.State == State.Quarantine && AssetInPortfolio.StateSetAt.AddDays(MAXIMUM_DAYS_IN_QUARANTINE) < DateTime.Now;
}
