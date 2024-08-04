using ErrorOr;
using IHolder.API.Mappers.Categories;
using IHolder.Application.Categories.Create;
using IHolder.Application.Categories.Delete;
using IHolder.Application.Categories.List;
using IHolder.Application.Categories.Update;
using IHolder.Contracts.Categories;
using IHolder.Domain.Categories;
using IHolder.SharedKernel.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IHolder.API.Controllers;

[Route("[controller]")]
public class CategoryController(ISender _mediator) : IHolderControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        CategoryGetByIdQuery command = new(id);

        ErrorOr<Category> category = await _mediator.Send(command);

        IActionResult response = category.Match(category => base.Ok(category.ToResponse()), Problem);

        return response;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginated([FromQuery] CategoryPaginatedListRequest request)
    {
        CategoryPaginatedListQuery query = request.ToPaginatedListQuery();

        ErrorOr<PaginatedList<Category>> paginatedList = await _mediator.Send(query);

        IActionResult response = paginatedList.Match(list => base.Ok(list.ToResponsePaginatedList()), Problem);

        return response;
    }

    [HttpPost()]
    public async Task<IActionResult> Create(CategoryCreateRequest request)
    {
        CategoryCreateCommand command = request.ToCreateCommand();

        ErrorOr<Category> category = await _mediator.Send(command);

        IActionResult response = category.Match(category => base.Ok(category.ToResponse()), Problem);

        return response;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, CategoryUpdateRequest request)
    {
        CategoryUpdateCommand command = request.ToUpdateCommand(id);

        ErrorOr<Category> category = await _mediator.Send(command);

        IActionResult response = category.Match(category => base.Ok(category.ToResponse()), Problem);

        return response;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        CategoryDeleteCommand command = new(id);

        ErrorOr<Deleted> result = await _mediator.Send(command);

        return result.Match(_ => NoContent(), Problem);
    }
}