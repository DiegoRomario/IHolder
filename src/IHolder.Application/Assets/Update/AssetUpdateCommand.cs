using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Assets;
using MediatR;

namespace IHolder.Application.Assets.Update;

[Authorization]
public record AssetUpdateCommand(
    Guid Id,
    Guid ProductId,
    string Name,
    string Description,
    string Ticker,
    decimal Price) : IRequest<ErrorOr<Asset>>;
