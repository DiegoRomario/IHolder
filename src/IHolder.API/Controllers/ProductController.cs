using ErrorOr;
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

namespace IHolder.API.Controllers;

[Route("[controller]")]
public class ProductController(ISender _mediator) : IHolderControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        ProductGetByIdQuery command = new(id);

        ErrorOr<Product> Product = await _mediator.Send(command);

        IActionResult response = Product.Match(Product => base.Ok(Product.ToProductResponse()), Problem);

        return response;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginated([FromQuery] ProductPaginatedListRequest request)
    {
        ProductPaginatedListQuery query = request.ToProductPaginatedListQuery();

        ErrorOr<PaginatedList<Product>> paginatedList = await _mediator.Send(query);

        IActionResult response = paginatedList.Match(list => base.Ok(list.ToProductResponsePaginatedList()), Problem);

        return response;
    }

    [HttpPost()]
    public async Task<IActionResult> Create(ProductCreateRequest request)
    {
        ProductCreateCommand command = request.ToProductCreateCommand();

        ErrorOr<Product> Product = await _mediator.Send(command);

        IActionResult response = Product.Match(Product => base.Ok(Product.ToProductResponse()), Problem);

        return response;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ProductUpdateRequest request)
    {
        ProductUpdateCommand command = request.ToProductUpdateCommand(id);

        ErrorOr<Product> Product = await _mediator.Send(command);

        IActionResult response = Product.Match(Product => base.Ok(Product.ToProductResponse()), Problem);

        return response;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        ProductDeleteCommand command = new(id);

        ErrorOr<Deleted> result = await _mediator.Send(command);

        return result.Match(_ => NoContent(), Problem);
    }
}