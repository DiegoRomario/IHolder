using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Portfolios;
using MediatR;

namespace IHolder.Application.Portfolios.Create;

[Authorization]
public record PortfolioCreateCommand(Guid UserId, string Name) : IRequest<ErrorOr<Portfolio>>;

