using ErrorOr;
using IHolder.API.Common;
using IHolder.Application.Common.Interfaces;
using IHolder.Application.Portfolios.AddAsset;
using IHolder.Application.Portfolios.List;
using IHolder.Application.Portfolios.RemoveAsset;
using IHolder.Application.Portfolios.SetAssetState;
using IHolder.Application.Portfolios.Update;
using IHolder.Application.Portfolios.UpdateAsset;
using IHolder.Contracts.Portfolios;
using IHolder.Domain.Portfolios;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IHolder.API.Portfolios;

[Route("[controller]")]
public class PortfoliosController(ISender _mediator, ICurrentUserProvider currentUserProvider) : IHolderControllerBase
{
    [HttpGet("{userId}")]
    public async Task<IActionResult> Get(Guid userId, CancellationToken ct)
    {
        PortfolioGetByUserIdQuery command = new(userId);

        ErrorOr<Portfolio> portfolio = await _mediator.Send(command, ct);

        IActionResult response = portfolio.Match(portfolio => base.Ok(portfolio.ToResponse()), Problem);

        return response;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, PortfolioUpdateRequest request, CancellationToken ct)
    {
        PortfolioUpdateCommand command = request.ToCommand(id);

        ErrorOr<Portfolio> portfolio = await _mediator.Send(command, ct);

        IActionResult response = portfolio.Match(portfolio => base.Ok(portfolio.ToResponse()), Problem);

        return response;
    }

    [HttpPost("{portfolioId}/assets")]
    public async Task<IActionResult> AddAsset(Guid portfolioId, PortfolioAddAssetRequest request, CancellationToken ct)
    {
        PortfolioAddAssetCommand command = request.ToCommand(portfolioId);

        ErrorOr<AssetInPortfolio> assetInPortfolio = await _mediator.Send(command, ct);

        Guid userId = currentUserProvider.GetCurrentUser().Value.Id;

        IActionResult response = assetInPortfolio.Match(asset => base.CreatedAtAction(nameof(Get), new { userId }, asset.ToResponse()), Problem);

        return response;
    }

    [HttpPut("{portfolioId}/assets/{assetInPortfolioId}")]
    public async Task<IActionResult> UpdateAsset(Guid portfolioId, Guid assetInPortfolioId, PortfolioUpdateAssetRequest request, CancellationToken ct)
    {
        PortfolioUpdateAssetCommand command = request.ToCommand(portfolioId, assetInPortfolioId);

        ErrorOr<AssetInPortfolio> portfolio = await _mediator.Send(command, ct);

        IActionResult response = portfolio.Match(portfolio => base.Ok(portfolio.ToResponse()), Problem);

        return response;
    }

    [HttpPatch("{portfolioId}/assets/{assetInPortfolioId}")]
    public async Task<IActionResult> UpdateAsset(Guid portfolioId, Guid assetInPortfolioId, PortfolioSetAssetStateRequest request, CancellationToken ct)
    {
        PortfolioSetAssetStateCommand command = request.ToCommand(portfolioId, assetInPortfolioId);

        ErrorOr<AssetInPortfolio> portfolio = await _mediator.Send(command, ct);

        IActionResult response = portfolio.Match(portfolio => base.Ok(portfolio.ToResponse()), Problem);

        return response;
    }

    [HttpDelete("{portfolioId}/assets/{assetInPortfolioId}")]
    public async Task<IActionResult> RemoveAsset(Guid portfolioId, Guid assetInPortfolioId, CancellationToken ct)
    {
        PortfolioRemoveAssetCommand command = new(portfolioId, assetInPortfolioId);

        ErrorOr<Deleted> result = await _mediator.Send(command, ct);

        return result.Match(_ => NoContent(), Problem);
    }
}