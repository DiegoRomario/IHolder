using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Assets;
using MediatR;

namespace IHolder.Application.Assets.Create;

[Authorization]
public record AssetCreateCommand(
    Guid ProductId,
    string Name,
    string Description,
    string Ticker,
    decimal Price) : IRequest<ErrorOr<Asset>>;

