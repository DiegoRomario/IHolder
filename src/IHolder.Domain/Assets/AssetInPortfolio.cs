using IHolder.Domain.Common;
using IHolder.Domain.Users;
using Throw;

namespace IHolder.Domain.Assets;

public class AssetInPortfolio : Entity
{
    private decimal _averagePrice;
    private decimal _quantity;

    public AssetInPortfolio(Guid assetId, decimal averagePrice, decimal quantity, Guid userId, DateTime? firstInvestmentDate = null)
    {
        AssetId = assetId;
        AveragePrice = averagePrice;
        Quantity = quantity;
        UserId = userId;
        FirstInvestmentDate = firstInvestmentDate ?? DateTime.Now;
    }

    private AssetInPortfolio() { }

    public Guid AssetId { get; private set; }

    /* Full getter and setter sintaxe here to ensure that InvestedAmount is recalculated whenever a new instance of AssetInPortfolio 
       is constructed using the default constructor (or when AveragePrice/Quantity is updated). */

    public decimal AveragePrice
    {
        get => _averagePrice;
        private set
        {
            _averagePrice = value;
            InvestedAmount = CalculateInvestedAmount(_averagePrice, _quantity);
        }
    }

    public decimal Quantity
    {
        get => _quantity;
        private set
        {
            _quantity = value;
            InvestedAmount = CalculateInvestedAmount(_averagePrice, _quantity);
        }
    }

    public decimal InvestedAmount { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime FirstInvestmentDate { get; private set; }
    public Asset Asset { get; private set; } = default!;
    public User User { get; private set; } = default!;

    private static decimal CalculateInvestedAmount(decimal averagePrice, decimal quantity)
    {
        // Check if averagePrice * quantity exceeds the maximum value for decimal
        bool investedAmountExceeded = (averagePrice != 0 && quantity != 0 && (decimal.MaxValue / Math.Abs(averagePrice) < Math.Abs(quantity)));
        investedAmountExceeded.Throw(paramName: $"{nameof(InvestedAmount)}").IfValueOverflowed();

        return averagePrice * quantity;
    }
}