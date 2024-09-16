using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Portfolios;
using MediatR;

namespace IHolder.Application.Portfolios.List;

public class PortfolioGetByUserIdQueryHandler(IPortfolioRepository _repository) : IRequestHandler<PortfolioGetByUserIdQuery, ErrorOr<Portfolio?>>
{
    public async Task<ErrorOr<Portfolio?>> Handle(PortfolioGetByUserIdQuery request, CancellationToken ct)
    {
        var portfolio = await _repository.GetByPredicateAsync(p => p.UserId == request.Id, ct, includes: true);

        if (portfolio is null) return Error.NotFound(description: "Portfolio not found");

        return portfolio;
    }
}
