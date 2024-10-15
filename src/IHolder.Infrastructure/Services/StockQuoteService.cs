using IHolder.Application.Common.Interfaces;
using IHolder.SharedKernel.DTO;
using System.Net.Http.Json;

namespace IHolder.Infrastructure.Services;

public class StockQuoteService(HttpClient _httpClient) : IAssetQuoteService
{
    // TODO: ADD LOGS
    public async Task<AssetQuoteDTO> GetAssetQuoteAsync(string ticker, string productDescription, CancellationToken cancellationToken)
    {
        var url = BuildRelativeQuoteUrl(ticker, productDescription);
        var response = await _httpClient.GetAsync(url, cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to fetch quote: {response.StatusCode}");

        var data = await response.Content.ReadFromJsonAsync<QuoteRoot>(cancellationToken);
        var meta = data?.Chart?.Result.FirstOrDefault()?.Meta;

        if (meta == null)
            throw new InvalidOperationException("Quote data not available.");

        var quote = new AssetQuote(meta.ChartPreviousClose, meta.RegularMarketPrice);
        return new AssetQuoteDTO(quote.PreviousQuote, quote.Quote, quote.Variation, quote.PercentageVariation);
    }

    private static string BuildRelativeQuoteUrl(string ticker, string productName)
    {
        // TODO: REVIEW CONDITION USING PRODUCT NAME 
        var symbol = productName.ToUpper() switch
        {
            "FII" or "AÇÃO" => $"{ticker}.SA",
            _ => ticker
        };

        // Build the relative path, as BaseAddress is already set
        return $"{symbol}?interval=1d";
    }
}
