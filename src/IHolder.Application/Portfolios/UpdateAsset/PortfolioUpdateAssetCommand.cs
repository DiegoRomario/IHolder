using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Portfolios;
using MediatR;

namespace IHolder.Application.Portfolios.UpdateAsset;

[Authorization]
public record PortfolioUpdateAssetCommand(
    Guid Id,
    Guid PortfolioId,
    Guid AssetId,
    decimal AveragePrice,
    decimal Quantity,
    DateTime FirstInvestmentDate) : IRequest<ErrorOr<AssetInPortfolio>>;
