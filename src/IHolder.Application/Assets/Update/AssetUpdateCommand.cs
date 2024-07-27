using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Assets;
using MediatR;

namespace IHolder.Application.Assets.Update;

[Authorization]
public record AssetUpdateCommand(
    Guid Id,
    Guid ProductId,
    string Description,
    string Details,
    string Ticker,
    decimal Price) : IRequest<ErrorOr<Asset>>;
