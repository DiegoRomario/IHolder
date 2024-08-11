using ErrorOr;
using IHolder.API.Common;
using IHolder.Application.Portfolios.List;
using IHolder.Application.Portfolios.Update;
using IHolder.Contracts.Portfolios;
using IHolder.Domain.Portfolios;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IHolder.API.Portfolios;

[Route("[controller]")]
public class PortfoliosController(ISender _mediator) : IHolderControllerBase
{
    [HttpGet("{userId}")]
    public async Task<IActionResult> Get(Guid userId, CancellationToken ct)
    {
        PortfolioGetByUserIdQuery command = new(userId);

        ErrorOr<Portfolio> Portfolio = await _mediator.Send(command, ct);

        IActionResult response = Portfolio.Match(Portfolio => base.Ok(Portfolio.ToResponse()), Problem);

        return response;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, PortfolioUpdateRequest request, CancellationToken ct)
    {
        PortfolioUpdateCommand command = request.ToUpdateCommand(id);

        ErrorOr<Portfolio> Portfolio = await _mediator.Send(command, ct);

        IActionResult response = Portfolio.Match(Portfolio => base.Ok(Portfolio.ToResponse()), Problem);

        return response;
    }
}