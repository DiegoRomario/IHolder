using ErrorOr;
using IHolder.Application.Common.Auth;
using MediatR;

namespace IHolder.Application.Portfolios.RemoveAsset;

[Authorization]
public record PortfolioRemoveAssetCommand(Guid PortfolioId, Guid Id) : IRequest<ErrorOr<Deleted>>;
