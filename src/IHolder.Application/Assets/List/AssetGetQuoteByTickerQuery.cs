using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Assets.List;

[Authorization]
public record AssetGetQuoteByTickerQuery(string Ticker) : IRequest<ErrorOr<AssetQuoteDTO?>>;
