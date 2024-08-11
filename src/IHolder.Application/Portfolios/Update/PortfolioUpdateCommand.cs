using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Portfolios;
using MediatR;

namespace IHolder.Application.Portfolios.Update;

[Authorization]
public record PortfolioUpdateCommand(Guid Id, Guid UserId, string Name) : IRequest<ErrorOr<Portfolio>>;
