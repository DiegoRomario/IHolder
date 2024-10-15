namespace IHolder.Infrastructure.Services;

public class AssetQuote
{
    public AssetQuote()
    {

    }
    public AssetQuote(decimal precoAnterior, decimal preco)
    {
        PreviousQuote = precoAnterior;
        Quote = preco;
        CalculateQuoteVariation();
    }

    public void CalculateQuoteVariation()
    {
        Variation = Quote - PreviousQuote;
        PercentageVariation = Variation / PreviousQuote * 100;
    }

    public decimal PreviousQuote { get; private set; }
    public decimal Quote { get; private set; }
    public decimal Variation { get; private set; }
    public decimal PercentageVariation { get; private set; }

}

public partial class QuoteRoot
{
    public Chart Chart { get; set; } = default!;
}

public partial class Chart
{
    public Result[] Result { get; set; } = [];

    public object Error { get; set; } = default!;
}

public partial class Result
{
    public Meta Meta { get; set; } = default!;

    public long[] Timestamp { get; set; } = [];

    public Indicators Indicators { get; set; } = default!;
}

public partial class Indicators
{
    public Quote[] Quote { get; set; } = [];

    public Adjclose[] Adjclose { get; set; } = [];
}

public partial class Adjclose
{
    public decimal[] AdjcloseAdjclose { get; set; } = [];
}

public partial class Quote
{
    public decimal[] High { get; set; } = [];
    public decimal[] Low { get; set; } = [];
    public long[] Volume { get; set; } = [];
    public decimal[] Close { get; set; } = [];
    public decimal[] Open { get; set; } = [];
}

public partial class Meta
{
    public string Currency { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public string ExchangeName { get; set; } = string.Empty;
    public string InstrumentType { get; set; } = string.Empty;
    public long FirstTradeDate { get; set; }
    public long RegularMarketTime { get; set; }
    public long Gmtoffset { get; set; }
    public string Timezone { get; set; } = string.Empty;
    public string ExchangeTimezoneName { get; set; } = string.Empty;
    public decimal RegularMarketPrice { get; set; }
    public decimal ChartPreviousClose { get; set; }
    public long PriceHint { get; set; }
    public CurrentTradingPeriod CurrentTradingPeriod { get; set; } = default!;
    public string DataGranularity { get; set; } = string.Empty;
    public string Range { get; set; } = string.Empty;
    public string[] ValidRanges { get; set; } = [];
}

public partial class CurrentTradingPeriod
{
    public Post Pre { get; set; } = default!;
    public Post Regular { get; set; } = default!;
    public Post Post { get; set; } = default!;
}

public partial class Post
{
    public string Timezone { get; set; } = string.Empty;
    public long Start { get; set; }
    public long End { get; set; }
    public long Gmtoffset { get; set; }
}