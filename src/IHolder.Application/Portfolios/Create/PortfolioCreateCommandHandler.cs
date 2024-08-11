using ErrorOr;
using IHolder.Application.Assets.Mappers;
using IHolder.Application.Common.Interfaces;
using IHolder.Application.Portfolios.Mappers;
using IHolder.Domain.Portfolios;
using MediatR;

namespace IHolder.Application.Portfolios.Create;

public class PortfolioCreateCommandHandler(IPortfolioRepository _repository) : IRequestHandler<PortfolioCreateCommand, ErrorOr<Portfolio>>
{
    public async Task<ErrorOr<Portfolio>> Handle(PortfolioCreateCommand request, CancellationToken ct)
    {
        var portfolio = request.ToEntity();

        await _repository.AddAsync(portfolio, ct);

        portfolio = await _repository.GetByUserIdAsync(portfolio.UserId, ct);

        if (portfolio == null) return Error.Conflict(description: "Failed to retrieve the created Portfolio.");

        return portfolio;
    }
}
