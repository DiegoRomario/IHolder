using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Assets;
using MediatR;

namespace IHolder.Application.Assets.Create;

[Authorization]
public record AssetCreateCommand(
    Guid ProductId,
    string Description,
    string Details,
    string Ticker,
    decimal Price) : IRequest<ErrorOr<Asset>>;

