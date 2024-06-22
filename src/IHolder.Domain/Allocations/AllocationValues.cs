using IHolder.Domain.Common;

namespace IHolder.Domain.Allocations;

public record AllocationValues(decimal TargetPercentage)
{
    public decimal TargetPercentage { get; private set; } = TargetPercentage;
    public decimal CurrentPercentage { get; private set; }
    public decimal PercentageDifference { get; private set; }
    public decimal CurrentAmount { get; private set; }
    public decimal AmountDifference { get; private set; }

    // TODO: REVIEW
    public void UpdateTargetPercentage(decimal targetPercentage)
    {
        TargetPercentage = targetPercentage.ToFloor();
    }

    public void RecalculateValues(decimal amountInvestedPerAllocation, decimal totalAmountInvested)
    {
        CurrentAmount = amountInvestedPerAllocation;
        CurrentPercentage = (totalAmountInvested == 0 ? 0 : CurrentAmount / totalAmountInvested * 100).ToFloor();
        PercentageDifference = (TargetPercentage - CurrentPercentage).ToFloor();
        UpdateAmountDifference(totalAmountInvested);

        void UpdateAmountDifference(decimal totalAmountInvested)
        {
            if (CurrentPercentage <= 0)
                AmountDifference = totalAmountInvested * TargetPercentage / 100;
            else
                AmountDifference = CurrentAmount / CurrentPercentage * PercentageDifference;
        }
    }
}

