using ErrorOr;
using IHolder.API.Mappers.Assets;
using IHolder.Application.Assets.Create;
using IHolder.Application.Assets.List;
using IHolder.Application.Assets.Update;
using IHolder.Contracts.Assets;
using IHolder.Domain.Assets;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IHolder.API.Controllers;

[Route("[controller]")]
public class AssetController(ISender _mediator) : IHolderControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        AssetGetByIdQuery command = new(id);

        ErrorOr<Asset> Asset = await _mediator.Send(command);

        IActionResult response = Asset.Match(Asset => base.Ok(Asset.ToAssetResponse()), Problem);

        return response;
    }

    [HttpPost()]
    public async Task<IActionResult> Create(AssetCreateRequest request)
    {
        AssetCreateCommand command = request.ToAssetCreateCommand();

        ErrorOr<Asset> Asset = await _mediator.Send(command);

        IActionResult response = Asset.Match(Asset => base.Ok(Asset.ToAssetResponse()), Problem);

        return response;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, AssetUpdateRequest request)
    {
        AssetUpdateCommand command = request.ToAssetUpdateCommand(id);

        ErrorOr<Asset> Asset = await _mediator.Send(command);

        IActionResult response = Asset.Match(Asset => base.Ok(Asset.ToAssetResponse()), Problem);

        return response;
    }
}