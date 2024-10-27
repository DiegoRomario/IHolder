using IHolder.Application.Common.Interfaces;
using IHolder.SharedKernel.DTO;
using System.Net.Http.Json;

namespace IHolder.Infrastructure.Services;

public class StockQuoteService(HttpClient _httpClient) : IAssetQuoteService
{
    // TODO: ADD LOGS
    public async Task<AssetQuoteDTO> GetAssetQuoteAsync(string ticker, string? exchangeId, CancellationToken cancellationToken)
    {
        var url = $"{ticker}{(string.IsNullOrEmpty(exchangeId) ? "" : $".{exchangeId}")}?interval=1d";
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
}
