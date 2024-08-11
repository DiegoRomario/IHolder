using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Portfolios;
using MediatR;

namespace IHolder.Application.Portfolios.List;

[Authorization]
public record PortfolioGetByUserIdQuery(Guid Id) : IRequest<ErrorOr<Portfolio?>>;
