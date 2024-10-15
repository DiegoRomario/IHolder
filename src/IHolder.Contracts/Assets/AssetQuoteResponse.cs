namespace IHolder.Contracts.Assets;

public record AssetQuoteResponse(decimal PreviousQuote, decimal Quote, decimal Variation, decimal PercentageVariation);
