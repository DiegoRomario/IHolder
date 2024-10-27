using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Assets.List;

public class AssetGetQuoteByTickerQueryHandler(IAssetQuoteService _service, IAssetRepository _assetRepository) : IRequestHandler<AssetGetQuoteByTickerQuery, ErrorOr<AssetQuoteDTO?>>
{
    public async Task<ErrorOr<AssetQuoteDTO?>> Handle(AssetGetQuoteByTickerQuery request, CancellationToken ct)
    {
        try
        {
            var exchangeId = await _assetRepository.GetExchangeIdByAssetTickerAsync(request.Ticker, ct);

            var assetQuote = await _service.GetAssetQuoteAsync(request.Ticker, exchangeId, ct);

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
