using ErrorOr;
using IHolder.Application.Assets.Mappers;
using IHolder.Application.Common.Interfaces;
using IHolder.Application.Portfolios.Mappers;
using IHolder.Domain.Portfolios;
using MediatR;

namespace IHolder.Application.Portfolios.Update;

public class PortfolioUpdateCommandHandler(IPortfolioRepository _repository) : IRequestHandler<PortfolioUpdateCommand, ErrorOr<Portfolio>>
{
    public async Task<ErrorOr<Portfolio>> Handle(PortfolioUpdateCommand request, CancellationToken ct)
    {
        if (await _repository.ExistsByPredicateAsync(a => a.Id == request.Id, ct) is false)
            return Error.Conflict(description: "Portfolio not found");

        await _repository.UpdateAsync(request.ToEntity(), ct);

        var portfolio = await _repository.GetByUserIdAsync(request.Id, ct);

        if (portfolio == null) return Error.Conflict(description: "Failed to retrieve the updated Portfolio.");

        return portfolio;
    }
}
