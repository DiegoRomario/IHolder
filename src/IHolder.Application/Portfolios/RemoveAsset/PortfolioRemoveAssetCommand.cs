using ErrorOr;
using IHolder.Application.Common.Auth;
using MediatR;

namespace IHolder.Application.Portfolios.AddAsset;

[Authorization]
public record PortfolioRemoveAssetCommand(Guid Id, Guid PortfolioId) : IRequest<ErrorOr<Deleted>>;
