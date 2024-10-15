using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Assets.List;

public class AssetGetQuoteByTickerQueryHandler(IAssetQuoteService _service, IAssetRepository _assetRepository) : IRequestHandler<AssetGetQuoteByTickerQuery, ErrorOr<AssetQuoteDTO?>>
{
    private const string errorMessage = "An unexpected error occurred while trying to get an asset quote.";

    public async Task<ErrorOr<AssetQuoteDTO?>> Handle(AssetGetQuoteByTickerQuery request, CancellationToken ct)
    {
        try
        {
            var productName = await _assetRepository.GetProductNameByAssetTickerAsync(request.Ticker, ct);

            if (productName is null) return Error.NotFound(description: $"Product name not found for the ticker: {request.Ticker}");

            var assetQuote = await _service.GetAssetQuoteAsync(request.Ticker, productName, ct);

            if (assetQuote is null) return Error.NotFound(description: "Asset quote not found");

            return assetQuote;
        }
        catch
        {
            // TODO: ADD LOGS
            return Error.Unexpected(description: $"An unexpected error occurred while trying to get an asset quote.");
        }
    }
}
