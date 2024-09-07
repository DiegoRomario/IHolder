using IHolder.Domain.Assets;
using IHolder.Domain.Common;
using IHolder.Domain.Enumerators;
using Throw;

namespace IHolder.Domain.Portfolios;

public class AssetInPortfolio : Entity
{
    private decimal _averagePrice;
    private decimal _quantity;

    public AssetInPortfolio(Guid portfolioId,
        Guid assetId,
        decimal averagePrice,
        decimal quantity,
        DateTime? firstInvestmentDate = null,
        State? state = null, Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        PortfolioId = portfolioId;
        AssetId = assetId;
        AveragePrice = averagePrice;
        Quantity = quantity;
        FirstInvestmentDate = firstInvestmentDate ?? DateTime.Now;
        State = state ?? State.Regular;
        StateSetAt = DateTime.Now;
    }

    private AssetInPortfolio() { }

    public Guid PortfolioId { get; private set; }
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
    public DateTime FirstInvestmentDate { get; private set; }
    public State State { get; private set; }
    public DateTime StateSetAt { get; private set; }
    public Asset Asset { get; private set; } = default!;
    public Portfolio Portfolio { get; private set; } = default!;

    public void SetState(State state)
    {
        State = state;
        StateSetAt = DateTime.Now;
    }

    public void UpdateQuantity(decimal quantity)
    {
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "The quantity of assets must be positive.");

        Quantity = quantity;
    }

    public void UpdateAveragePrice(decimal averagePrice)
    {
        if (averagePrice <= 0) throw new ArgumentOutOfRangeException(nameof(averagePrice), "The average price of the asset must be positive.");

        AveragePrice = averagePrice;
    }

    public void UpdateFirstInvestmentDate(DateTime firstInvestmentDate)
    {
        FirstInvestmentDate = firstInvestmentDate;
    }

    private static decimal CalculateInvestedAmount(decimal averagePrice, decimal quantity)
    {
        // Check if averagePrice * quantity exceeds the maximum value for decimal
        bool investedAmountExceeded = averagePrice != 0 && quantity != 0 && decimal.MaxValue / Math.Abs(averagePrice) < Math.Abs(quantity);
        investedAmountExceeded.Throw(paramName: $"{nameof(InvestedAmount)}").IfValueOverflowed();

        return averagePrice * quantity;
    }
}