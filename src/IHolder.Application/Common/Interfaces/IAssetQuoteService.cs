using IHolder.SharedKernel.DTO;

namespace IHolder.Application.Common.Interfaces;

public interface IAssetQuoteService
{
    Task<AssetQuoteDTO> GetAssetQuoteAsync(string ticker, string? exchangeId, CancellationToken cancellationToken = default);
}
