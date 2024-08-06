using ErrorOr;
using IHolder.API.Controllers;
using IHolder.API.Mappers.Products;
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
public class ProductController(ISender _mediator) : IHolderControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken ct)
    {
        ProductGetByIdQuery command = new(id);

        ErrorOr<Product> Product = await _mediator.Send(command, ct);

        IActionResult response = Product.Match(Product => base.Ok(Product.ToResponse()), Problem);

        return response;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginated([FromQuery] ProductPaginatedListRequest request, CancellationToken ct)
    {
        ProductPaginatedListQuery query = request.ToPaginatedListQuery();

        ErrorOr<PaginatedList<Product>> paginatedList = await _mediator.Send(query, ct);

        IActionResult response = paginatedList.Match(list => base.Ok(list.ToResponsePaginatedList()), Problem);

        return response;
    }

    [HttpPost()]
    public async Task<IActionResult> Create(ProductCreateRequest request, CancellationToken ct)
    {
        ProductCreateCommand command = request.ToCreateCommand();

        ErrorOr<Product> Product = await _mediator.Send(command, ct);

        IActionResult response = Product.Match(Product => base.Ok(Product.ToResponse()), Problem);

        return response;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ProductUpdateRequest request, CancellationToken ct)
    {
        ProductUpdateCommand command = request.ToUpdateCommand(id);

        ErrorOr<Product> Product = await _mediator.Send(command, ct);

        IActionResult response = Product.Match(Product => base.Ok(Product.ToResponse()), Problem);

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