using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Enumerators;
using IHolder.Domain.Portfolios;
using MediatR;

namespace IHolder.Application.Portfolios.SetAssetState;

[Authorization]
public record PortfolioSetAssetStateCommand(Guid PortfolioId, Guid Id, State State) : IRequest<ErrorOr<AssetInPortfolio>>;
