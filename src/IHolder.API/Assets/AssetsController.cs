using ErrorOr;
using IHolder.API.Controllers;
using IHolder.API.Mappers.Assets;
using IHolder.Application.Assets.Create;
using IHolder.Application.Assets.List;
using IHolder.Application.Assets.Update;
using IHolder.Contracts.Assets;
using IHolder.Domain.Assets;
using IHolder.SharedKernel.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IHolder.API.Assets;

[Route("[controller]")]
public class AssetsController(ISender _mediator) : IHolderControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken ct)
    {
        AssetGetByIdQuery command = new(id);

        ErrorOr<Asset> Asset = await _mediator.Send(command, ct);

        IActionResult response = Asset.Match(Asset => base.Ok(Asset.ToResponse()), Problem);

        return response;
    }


    [HttpGet]
    public async Task<IActionResult> GetPaginated([FromQuery] AssetPaginatedListRequest request, CancellationToken ct)
    {
        AssetPaginatedListQuery query = request.ToPaginatedListQuery();

        ErrorOr<PaginatedList<Asset>> paginatedList = await _mediator.Send(query, ct);

        IActionResult response = paginatedList.Match(list => base.Ok(list.ToResponsePaginatedList()), Problem);

        return response;
    }

    [HttpPost()]
    public async Task<IActionResult> Create(AssetCreateRequest request, CancellationToken ct)
    {
        AssetCreateCommand command = request.ToCreateCommand();

        ErrorOr<Asset> Asset = await _mediator.Send(command, ct);

        IActionResult response = Asset.Match(Asset => base.Ok(Asset.ToResponse()), Problem);

        return response;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, AssetUpdateRequest request, CancellationToken ct)
    {
        AssetUpdateCommand command = request.ToUpdateCommand(id);

        ErrorOr<Asset> Asset = await _mediator.Send(command, ct);

        IActionResult response = Asset.Match(Asset => base.Ok(Asset.ToResponse()), Problem);

        return response;
    }
}