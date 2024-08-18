using ErrorOr;
using IHolder.API.Common;
using IHolder.Application.Products.Create;
using IHolder.Application.Products.Delete;
using IHolder.Application.Products.List;
using IHolder.Application.Products.Update;
using IHolder.Contracts.Products;
using IHolder.Domain.Products;
using IHolder.SharedKernel.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IHolder.API.Products;

[Route("[controller]")]
public class ProductsController(ISender _mediator) : IHolderControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken ct)
    {
        ProductGetByIdQuery command = new(id);

        ErrorOr<Product> product = await _mediator.Send(command, ct);

        IActionResult response = product.Match(product => base.Ok(product.ToResponse()), Problem);

        return response;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginated([FromQuery] ProductPaginatedListRequest request, CancellationToken ct)
    {
        ProductsPaginatedListQuery query = request.ToQuery();

        ErrorOr<PaginatedList<Product>> paginatedList = await _mediator.Send(query, ct);

        IActionResult response = paginatedList.Match(list => base.Ok(list.ToResponse()), Problem);

        return response;
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateRequest request, CancellationToken ct)
    {
        ProductCreateCommand command = request.ToCommand();

        ErrorOr<Product> product = await _mediator.Send(command, ct);

        IActionResult response = product.Match(product => base.CreatedAtAction(nameof(Get), new { id = product.Id }, product.ToResponse()), Problem);

        return response;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ProductUpdateRequest request, CancellationToken ct)
    {
        ProductUpdateCommand command = request.ToCommand(id);

        ErrorOr<Product> product = await _mediator.Send(command, ct);

        IActionResult response = product.Match(product => base.Ok(product.ToResponse()), Problem);

        return response;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        ProductDeleteCommand command = new(id);

        ErrorOr<Deleted> result = await _mediator.Send(command, ct);

        return result.Match(_ => NoContent(), Problem);
    }
}